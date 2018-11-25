using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Scheduler.Core.Attribute;
using Scheduler.Core.Entity;
using Scheduler.Core.Enum;
using Scheduler.Core.Library;
using Scheduler.Model;
using Scheduler.Quartz;

namespace Scheduler.Main.Controllers
{

    public class TaskController : BaseController
    {
        private readonly SchedulerServer _schedulerServer;
        private IConfiguration Configuration { get; }

        public TaskController(SchedulerServer schedulerServer, IConfiguration configuration)
        {
            _schedulerServer = schedulerServer;
            Configuration = configuration;
        }

        [HttpGet]
        [Auth(AllowLevel = IdentityLevel.All)]
        public IActionResult Index()
        {
            var page = GetParam<int>("page", 1);
            using (var db = new BaseModel())
            {
                var query = from taskModel in db.Task
                    join userModel in db.User on taskModel.UserId equals userModel.Id into t1
                    from t1Model in t1.DefaultIfEmpty()
                    join groupModel in db.Group on taskModel.GroupId equals groupModel.Id into t2
                    from t2Model in t2.DefaultIfEmpty()
                    select new TaskInfoEntity
                    {
                        Id = taskModel.Id,
                        Name = taskModel.Name,
                        Description = taskModel.Description,
                        UserId = taskModel.UserId,
                        UserName = t1Model.UserName,
                        TriggerId = taskModel.TriggerId,
                        TriggerValue = taskModel.TriggerValue,
                        TriggerDesc = taskModel.TriggerDesc,
                        Type = taskModel.Type,
                        Value = taskModel.Value,
                        Status = taskModel.Status,
                        CreatedTime = taskModel.CreatedTime,
                        GroupName = t2Model.Name
                    };
                if (!Auth.IsAdmin)
                {
                    query = query.Where(rs => rs.UserId == Auth.Info.Id);
                }

                var result = query.OrderByDescending(rs => rs.Id).Skip(PageNum * (page - 1)).Take(PageNum).ToList();
                var total = Auth.IsAdmin
                    ? db.Task.Count()
                    : db.Task.Count(rs => rs.UserId == Auth.Info.Id);
                ViewBag.TaskList = result;
                ViewBag.TaskTotal = total;
                ViewBag.PageNum = PageNum;
                ViewBag.CurrentPage = page;
            }

            return View();
        }

        [HttpGet]
        [Auth(AllowLevel = IdentityLevel.All)]
        public IActionResult Add()
        {
            using (var db = new BaseModel())
            {
                ViewBag.TriggerList = db.Trigger.Where(rs => rs.UserId == Auth.Info.Id).OrderByDescending(rs => rs.Id)
                    .ToList();
                ViewBag.GroupList = db.Group.OrderByDescending(rs => rs.Id).ToList();
            }

            return View();
        }

        [HttpPost]
        [Auth(AllowLevel = IdentityLevel.All)]
        public JsonResult AddDo()
        {
            var name = GetParam<string>("inputName");
            var desc = GetParam<string>("inputDescription");
            var type = GetParam<int>("selectType");
            var value = GetParam<string>("inputValue");
            var triggerId = GetParam<int>("selectTrigger");
            var groupId = GetParam<int>("selectGroup");
            Request.Validation(
                new ValidationEntity {Key = name, Des = "任务名称不可为空", Type = ValidationType.Required},
                new ValidationEntity {Key = triggerId, Des = "触发器ID不可小于零", Type = ValidationType.IdInt},
                new ValidationEntity {Key = value, Des = "任务值不可为空", Type = ValidationType.Required}
            );
            using (var db = new BaseModel())
            {
                var triggerInfo = db.Trigger.FirstOrDefault(rs => rs.Id == triggerId && rs.UserId == Auth.Info.Id);
                if (triggerInfo == null)
                {
                    return Tools.ReJson("获取触发器信息失败");
                }

                var groupInfo = db.Group.FirstOrDefault(rs => rs.Id == groupId);
                if (groupInfo == null)
                {
                    return Tools.ReJson("获取任务组信息失败");
                }

                db.Task.Add(new TaskModel
                {
                    Name = name,
                    GroupId = groupId,
                    Description = desc,
                    TriggerId = triggerId,
                    TriggerValue = triggerInfo.Value,
                    TriggerDesc = triggerInfo.Name,
                    UserId = Auth.Info.Id,
                    Type = type,
                    Value = value,
                    Status = 0,
                    CreatedTime = DateTime.Now.ToLocalTime(),
                    UpdatedTime = DateTime.Now.ToLocalTime(),
                });
                if (db.SaveChanges() <= 0)
                {
                    return Tools.ReJson("添加任务失败");
                }
            }

            return Tools.ReJson();
        }

        [HttpGet]
        [Auth(AllowLevel = IdentityLevel.All)]
        public IActionResult Edit()
        {
            var id = GetParam<int>("id");
            Request.Validation(new ValidationEntity {Key = id, Des = "ID不可小于零", Type = ValidationType.IdInt});
            using (var db = new BaseModel())
            {
                var result = Auth.IsAdmin
                    ? db.Task.FirstOrDefault(rs => rs.Id == id)
                    : db.Task.FirstOrDefault(rs => rs.Id == id && rs.UserId == Auth.Info.Id);
                if (result == null)
                {
                    return Tools.ReJson("获取任务信息失败");
                }

                ViewBag.TaskInfo = result;
                ViewBag.TriggerList = db.Trigger.Where(rs => rs.UserId == Auth.Info.Id).OrderByDescending(rs => rs.Id)
                    .ToList();
                ViewBag.GroupList = db.Group.OrderByDescending(rs => rs.Id).ToList();
            }

            return View();
        }

        [HttpPost]
        [Auth(AllowLevel = IdentityLevel.All)]
        public JsonResult EditDo()
        {
            var id = GetParam<int>("inputId");
            var name = GetParam<string>("inputName");
            var desc = GetParam<string>("inputDescription");
            var type = GetParam<int>("selectType");
            var value = GetParam<string>("inputValue");
            var triggerId = GetParam<int>("selectTrigger");
            var groupId = GetParam<int>("selectGroup");
            Request.Validation(
                new ValidationEntity {Key = id, Des = "ID不可小于零", Type = ValidationType.IdInt},
                new ValidationEntity {Key = name, Des = "任务名称不可为空", Type = ValidationType.Required},
                new ValidationEntity {Key = value, Des = "任务值不可为空", Type = ValidationType.Required}
            );
            using (var db = new BaseModel())
            {
                var result = Auth.IsAdmin
                    ? db.Task.FirstOrDefault(rs => rs.Id == id)
                    : db.Task.FirstOrDefault(rs => rs.Id == id && rs.UserId == Auth.Info.Id);
                if (result == null)
                {
                    return Tools.ReJson("获取任务信息失败");
                }

                if (result.Status == 1)
                {
                    return Tools.ReJson("任务正在运行中，请先停用");
                }

                var resetJob = false;
                //如果选择了触发器
                if (triggerId > 0)
                {
                    var triggerInfo = db.Trigger.FirstOrDefault(rs => rs.Id == triggerId && rs.UserId == Auth.Info.Id);
                    if (triggerInfo == null)
                    {
                        return Tools.ReJson("获取触发器信息失败");
                    }

                    if (Tools.Md5(triggerInfo.Value) != Tools.Msg(result.TriggerValue))
                    {
                        result.TriggerValue = triggerInfo.Value;
                        result.Description = triggerInfo.Name;
                        result.TriggerId = triggerId;
                        result.Status = 0;
                        resetJob = true;
                    }
                }

                if (type != result.Type)
                {
                    result.Type = type;
                    resetJob = true;
                }

                if (value != result.Value)
                {
                    result.Value = value;
                    resetJob = true;
                }

                result.Name = name;
                result.GroupId = groupId;
                result.Description = desc;
                db.Task.Update(result);
                if (db.SaveChanges() <= 0)
                {
                    return Tools.ReJson("修改任务失败");
                }

                //如果更改了触发器|任务类型|任务值
                if (resetJob)
                {

                }
            }

            return Tools.ReJson();
        }

        [HttpPost]
        [Auth(AllowLevel = IdentityLevel.Admin)]
        public JsonResult DelDo()
        {
            var id = GetParam<int>("id");
            Request.Validation(new ValidationEntity {Key = id, Des = "ID不可小于零", Type = ValidationType.IdInt});
            using (var db = new BaseModel())
            {
                var result = db.Task.FirstOrDefault(rs => rs.Id == id);
                if (result == null)
                {
                    return Tools.ReJson("获取任务信息失败");
                }

                db.Task.Remove(result);
                if (db.SaveChanges() <= 0)
                {
                    return Tools.ReJson("删除任务信息失败");
                }
            }

            return Tools.ReJson(_schedulerServer.RemoveJob(id.ToString()));
        }

        [HttpPost]
        [Auth(AllowLevel = IdentityLevel.Admin)]
        public JsonResult StatusDo()
        {
            var id = GetParam<int>("id");
            var status = GetParam<int>("status") == 1 ? 1 : 0;
            Request.Validation(new ValidationEntity {Key = id, Des = "ID不可小于零", Type = ValidationType.IdInt});
            using (var db = new BaseModel())
            {
                var result = db.Task.FirstOrDefault(rs => rs.Id == id);
                if (result == null)
                {
                    return Tools.ReJson("获取任务信息失败");
                }

                result.Status = status;
                db.Task.Update(result);
                if (db.SaveChanges() <= 0)
                {
                    return Tools.ReJson("更改任务信息失败");
                }

                var userInfo = db.User.FirstOrDefault(rs => rs.Id == result.UserId);
                if (userInfo == null)
                {
                    return Tools.ReJson("获取用户信息失败");
                }

                var groupInfo = db.Group.FirstOrDefault(rs => rs.Id == result.GroupId);
                if (groupInfo == null)
                {
                    return Tools.ReJson("获取任务组信息失败");
                }

                var emailEntity = new EmailEntity();
                Configuration.GetSection("email").Bind(emailEntity);

                if (status == 1)
                {
                    _schedulerServer.AddJob(new SchedulerJobEntity
                        {
                            Key = result.Id.ToString(),
                            TaskInfo = new TaskInfoEntity
                            {
                                Id = result.Id,
                                Name = result.Name,
                                Description = result.Description,
                                UserId = result.UserId,
                                UserName = userInfo.UserName,
                                UserEmail = userInfo.Email,
                                TriggerId = result.TriggerId,
                                TriggerValue = result.TriggerValue,
                                TriggerDesc = result.TriggerDesc,
                                Type = result.Type,
                                Value = result.Value,
                                Status = result.Status,
                                CreatedTime = result.CreatedTime,
                                GroupName = groupInfo.Name
                            },
                            EmailInfo = emailEntity
                        },
                        new SchedulerTriggerEntity
                        {
                            Key = result.Id + "-" + result.TriggerId,
                            Desc = result.TriggerDesc,
                            Rule = result.TriggerValue
                        }).GetAwaiter().GetResult();
                }
                else
                {
                    _schedulerServer.RemoveJob(id.ToString()).GetAwaiter().GetResult();
                }
            }

            return Tools.ReJson();
        }

        [HttpPost]
        [Auth(AllowLevel = IdentityLevel.All)]
        public JsonResult RunDo()
        {
            var id = GetParam<int>("id");
            Request.Validation(new ValidationEntity {Key = id, Des = "ID不可小于零", Type = ValidationType.IdInt});
            using (var db = new BaseModel())
            {
                var result = db.Task.FirstOrDefault(rs => rs.Id == id);
                if (result == null)
                {
                    return Tools.ReJson("获取任务信息失败");
                }

                if (result.Status == 0)
                {
                    return Tools.ReJson("此任务未启用");
                }
            }

            return Tools.ReJson(_schedulerServer.RunJob(id.ToString()));
        }

        [HttpPost]
        [Auth(AllowLevel = IdentityLevel.All)]
        public JsonResult PauseDo()
        {
            var id = GetParam<string>("id");
            var taskId = GetParam<int>("taskId");
            Request.Validation(new ValidationEntity {Key = id, Des = "ID不可为空", Type = ValidationType.Required},
                new ValidationEntity {Key = taskId, Des = "任务ID不可小于零", Type = ValidationType.IdInt});
            using (var db = new BaseModel())
            {
                var result = db.Task.FirstOrDefault(rs => rs.Id == taskId);
                if (result == null)
                {
                    return Tools.ReJson("获取任务信息失败");
                }

                if (result.Status == 0)
                {
                    return Tools.ReJson("此任务未启用");
                }
            }

            return Tools.ReJson(_schedulerServer.PauseTrigger(id));
        }

        [HttpPost]
        [Auth(AllowLevel = IdentityLevel.All)]
        public JsonResult ResumeDo()
        {
            var id = GetParam<string>("id");
            var taskId = GetParam<int>("taskId");
            Request.Validation(new ValidationEntity {Key = id, Des = "ID不可为空", Type = ValidationType.Required},
                new ValidationEntity {Key = taskId, Des = "任务ID不可小于零", Type = ValidationType.IdInt});
            using (var db = new BaseModel())
            {
                var result = db.Task.FirstOrDefault(rs => rs.Id == taskId);
                if (result == null)
                {
                    return Tools.ReJson("获取任务信息失败");
                }

                if (result.Status == 0)
                {
                    return Tools.ReJson("此任务未启用");
                }
            }

            return Tools.ReJson(_schedulerServer.ResumeTrigger(id));
        }

        [HttpGet]
        [Auth(AllowLevel = IdentityLevel.All)]
        public IActionResult Logger()
        {
            return View();
        }

        [HttpGet]
        [Auth(AllowLevel = IdentityLevel.All)]
        public JsonResult GetLogger()
        {
            var id = GetParam<int>("id");
            var page = GetParam<int>("page", 1);
            Request.Validation(new ValidationEntity {Key = id, Des = "ID不可为空", Type = ValidationType.Required});
            using (var db = new BaseModel())
            {
                var result = db.Logger.Where(rs => rs.TaskId == id).OrderByDescending(rs => rs.Id)
                    .Skip(PageNum * (page - 1)).Take(PageNum).ToList();
                var total = db.Logger.Count(rs => rs.TaskId == id);
                return Tools.ReJson(new
                {
                    list = result,
                    total = total,
                    pageNum = PageNum,
                    currentPage = page
                });

            }
        }
    }
}