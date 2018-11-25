using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Core.Attribute;
using Scheduler.Core.Entity;
using Scheduler.Core.Enum;
using Scheduler.Core.Library;
using Scheduler.Model;
using Scheduler.Quartz;

namespace Scheduler.Main.Controllers
{
    [Auth(AllowLevel = IdentityLevel.All)]
    public class TriggerController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetList()
        {
            var page = GetParam<int>("page", 1);
            using (var db = new BaseModel())
            {
                var query = from triggerModel in db.Trigger
                    join userModel in db.User on triggerModel.UserId equals userModel.Id into t
                    from tempModel in t.DefaultIfEmpty()
                    select new
                    {
                        Id = triggerModel.Id,
                        UserName = tempModel.UserName,
                        UserId = triggerModel.UserId,
                        Name = triggerModel.Name,
                        Value = triggerModel.Value,
                        CreatedTime = triggerModel.CreatedTime,
                        UpdatedTime = triggerModel.UpdatedTime
                    };
                if (!Auth.IsAdmin)
                {
                    query = query.Where(rs => rs.UserId == Auth.Info.Id);
                }
                var result = query.OrderByDescending(rs => rs.Id).Skip(PageNum * (page - 1)).Take(PageNum).ToList();
                var total = Auth.IsAdmin
                    ? db.Trigger.Count()
                    : db.Trigger.Count(rs => rs.UserId == Auth.Info.Id);
                return Tools.ReJson(new
                {
                    list = result,
                    total = total,
                    pageNum = PageNum,
                    currentPage = page
                });

            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddDo()
        {
            var name = GetParam<string>("inputName");
            var value = GetParam<string>("inputExpCron");
            Request.Validation(
                new ValidationEntity {Key = name, Des = "触发器名称不得为空", Type = ValidationType.Required},
                new ValidationEntity {Key = value, Des = "触发器规则不得为空", Type = ValidationType.Required}
            );
            if (SchedulerServer.FireTimeList(value, 1).Count <= 0)
            {
                return Tools.ReJson("触发器规则不正确");
            }
            using (var db = new BaseModel())
            {
                db.Trigger.Add(new TriggerModel
                {
                    Name = name,
                    UserId = Auth.Info.Id,
                    Value = value,
                    CreatedTime = DateTime.Now.ToLocalTime(),
                    UpdatedTime = DateTime.Now.ToLocalTime()
                });
                if (db.SaveChanges() <= 0)
                {
                    return Tools.ReJson("添加触发器失败");
                }
            }
            return Tools.ReJson();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var id = GetParam<int>("id");
            Request.Validation(new ValidationEntity {Key = id, Des = "ID不可小于零", Type = ValidationType.IdInt});
            using (var db = new BaseModel())
            {
                var result = Auth.IsAdmin
                    ? db.Trigger.FirstOrDefault(rs => rs.Id == id)
                    : db.Trigger.FirstOrDefault(rs => rs.Id == Auth.Info.Id);
                if (result == null)
                {
                    return Tools.ReJson("获取触发器信息失败");
                }

                ViewBag.TriggerInfo = result;
            }
            return View();
        }

        [HttpPost]
        public JsonResult EditDo()
        {
            var id = GetParam<int>("inputId");
            var name = GetParam<string>("inputName");
            var value = GetParam<string>("inputExpCron");
            Request.Validation(
                new ValidationEntity {Key = id, Des = "ID不可小于零", Type = ValidationType.IdInt},
                new ValidationEntity {Key = name, Des = "触发器名称不得为空", Type = ValidationType.Required},
                new ValidationEntity {Key = value, Des = "触发器规则不得为空", Type = ValidationType.Required}
            );
            if (SchedulerServer.FireTimeList(value, 1).Count <= 0)
            {
                return Tools.ReJson("触发器规则不正确");
            }

            using (var db = new BaseModel())
            {
                var result = Auth.IsAdmin
                    ? db.Trigger.FirstOrDefault(rs => rs.Id == id)
                    : db.Trigger.FirstOrDefault(rs => rs.Id == id && rs.UserId == Auth.Info.Id);
                if (result == null)
                {
                    return Tools.ReJson("获取触发器信息失败");
                }

                result.Name = name;
                result.Value = value;
                db.Trigger.Update(result);
                if (db.SaveChanges() <= 0)
                {
                    return Tools.ReJson("更新触发器信息失败");
                }
            }

            return Tools.ReJson();
        }

        [HttpPost]
        public JsonResult DelDo()
        {
            var id = GetParam<int>("id");
            Request.Validation(new ValidationEntity {Key = id, Des = "ID不可小于零", Type = ValidationType.IdInt});
            using (var db = new BaseModel())
            {
                var result = Auth.IsAdmin
                    ? db.Trigger.FirstOrDefault(rs => rs.Id == id)
                    : db.Trigger.FirstOrDefault(rs => rs.Id == id && rs.UserId == Auth.Info.Id);
                if (result == null)
                {
                    return Tools.ReJson("获取触发器信息失败");
                }
                db.Trigger.Remove(result);
                if (db.SaveChanges() <= 0)
                {
                    return Tools.ReJson("删除触发器信息失败");
                }
            }

            return Tools.ReJson();
        }

        [HttpGet]
        public JsonResult TestRule()
        {
            var rule = GetParam<string>("rule");
            var count = GetParam<int>("count", 1);
            var list = SchedulerServer.FireTimeList(rule, count);
            return list.Count <= 0 ? Tools.ReJson("触发器规则不正确") : Tools.ReJson(list);
        }
    }
}