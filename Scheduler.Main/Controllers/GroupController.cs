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

namespace Scheduler.Main.Controllers
{
    [Auth(AllowLevel = IdentityLevel.Admin)]
    public class GroupController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            using (var db = new BaseModel())
            {
                ViewBag.GroupList = db.Group.OrderByDescending(rs => rs.Id).ToList();
            }

            return View();
        }

        [HttpPost]
        public JsonResult AddDo()
        {
            var name = GetParam<string>("name");
            Request.Validation(new ValidationEntity {Key = name, Des = "任务组名称不可为空", Type = ValidationType.Required});
            using (var db = new BaseModel())
            {
                if (db.Group.Count(rs => rs.Name == name) > 0)
                {
                    return Tools.ReJson("任务组名称已存在");
                }

                db.Group.Add(new GroupModel
                {
                    Name = name
                });
                if (db.SaveChanges() <= 0)
                {
                    return Tools.ReJson("添加任务组失败");
                }
            }

            return Tools.ReJson();
        }

        [HttpPost]
        public JsonResult EditDo()
        {
            var id = GetParam<int>("id");
            var name = GetParam<string>("name");
            Request.Validation(
                new ValidationEntity {Key = id, Des = "ID不可小于零", Type = ValidationType.IdInt},
                new ValidationEntity {Key = name, Des = "任务组名称不可为空", Type = ValidationType.Required}
            );
            using (var db = new BaseModel())
            {
                if (db.Group.Count(rs => rs.Id != id && rs.Name == name) > 0)
                {
                    return Tools.ReJson("任务组名称已存在");
                }

                var result = db.Group.FirstOrDefault(rs => rs.Id == id);
                if (result == null)
                {
                    return Tools.ReJson("获取任务组信息失败");
                }

                result.Name = name;
                db.Group.Update(result);
                if (db.SaveChanges() <= 0)
                {
                    return Tools.ReJson("更新任务组信息失败");
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
                if (db.Task.Count(rs => rs.GroupId == id) > 0)
                {
                    return Tools.ReJson("该任务组存在下属任务不可删除");
                }

                var result = db.Group.FirstOrDefault(rs => rs.Id == id);
                if (result == null)
                {
                    return Tools.ReJson("获取任务组信息失败");
                }

                db.Group.Remove(result);
                if (db.SaveChanges() <= 0)
                {
                    return Tools.ReJson("删除任务组信息失败");
                }
            }

            return Tools.ReJson();
        }
    }
}