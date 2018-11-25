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
    public class UserController : BaseController
    {
        [HttpGet]
        [Auth(AllowLevel = IdentityLevel.Admin)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Auth(AllowLevel = IdentityLevel.Normal)]
        public IActionResult Info()
        {
            using (var db = new BaseModel())
            {
                var result = db.User.FirstOrDefault(rs => rs.Id == Auth.Info.Id);
                if (result == null)
                {
                    return Tools.ReJson("获取用户信息失败");
                }

                ViewBag.UserInfo = result;
            }
            return View();
        }

        [HttpPost]
        [Auth(AllowLevel = IdentityLevel.Normal)]
        public JsonResult InfoDo()
        {
            var passWord = GetParam<string>("inputPass");
            var email = GetParam<string>("inputEmail");
            var mobile = GetParam<string>("inputMobile");
            Request.Validation(new ValidationEntity { Key = email, Des = "邮箱格式不正在确", Type = ValidationType.Email });
            using (var db = new BaseModel())
            {
                var result = db.User.FirstOrDefault(rs => rs.Id == Auth.Info.Id);
                if (result == null)
                {
                    return Tools.ReJson("获取用户信息失败");
                }
                if (!string.IsNullOrWhiteSpace(passWord))
                {
                    result.PassWord = Tools.Md5(passWord);
                }
                result.Email = email;
                result.Mobile = mobile;
                db.User.Update(result);
                if (db.SaveChanges() <= 0)
                {
                    return Tools.ReJson("更新用户信息失败");
                }
                ClearAuth();
            }
            return Tools.ReJson();
        }

        [HttpGet]
        [Auth(AllowLevel = IdentityLevel.Admin)]
        public JsonResult GetList()
        {
            var page = GetParam<int>("page", 1);
            using (var db = new BaseModel())
            {
                var result = db.User.OrderByDescending(rs => rs.Level).ThenByDescending(rs => rs.Id).Skip(PageNum * (page - 1))
                    .Take(PageNum).ToList();
                return Tools.ReJson(new
                {
                    list = result,
                    total = db.User.Count(),
                    pageNum = PageNum,
                    currentPage = page
                });
            }
        }

        [HttpGet]
        [Auth(AllowLevel = IdentityLevel.Admin)]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Auth(AllowLevel = IdentityLevel.Admin)]
        public JsonResult AddDo()
        {
            var userName = GetParam<string>("inputName");
            var passWord = GetParam<string>("inputPass");
            var email = GetParam<string>("inputEmail");
            var mobile = GetParam<string>("inputMobile");
            var status = GetParam<string>("inputStatus") == "on" ? 1 : 0;
            var level = GetParam<int>("selectLevel") == 2 ? 2 : 1;
            Request.Validation(
                new ValidationEntity { Key = userName, Des = "用户名不得为空", Type = ValidationType.Required },
                new ValidationEntity { Key = passWord, Des = "密码不得为空", Type = ValidationType.Required },
                new ValidationEntity { Key = email, Des = "邮箱格式不正在确", Type = ValidationType.Email }
                );
            using (var db = new BaseModel())
            {
                if (db.User.Count(rs => rs.UserName == userName) > 0)
                {
                    return Tools.ReJson("该用户名已存在");
                }
                db.User.Add(new UserModel
                {
                    UserName = userName,
                    PassWord = Tools.Md5(passWord),
                    Level = level,
                    Mobile = mobile,
                    Email = email,
                    Status = status,
                    CreatedTime = DateTime.Now.ToLocalTime(),
                    UpdatedTime = DateTime.Now.ToLocalTime()
                });
                if (db.SaveChanges() <= 0)
                {
                    return Tools.ReJson("添加用户失败");
                }
            }
            return Tools.ReJson();
        }

        [HttpGet]
        [Auth(AllowLevel = IdentityLevel.Admin)]
        public IActionResult Edit()
        {
            var id = GetParam<int>("id");
            Request.Validation(new ValidationEntity {Key = id, Des = "ID不可小于零", Type = ValidationType.IdInt});
            using (var db = new BaseModel())
            {
                var result = db.User.FirstOrDefault(rs => rs.Id == id);
                if (result == null)
                {
                    return Tools.ReJson("获取用户信息失败");
                }

                ViewBag.UserInfo = result;
            }

            return View();
        }

        [HttpPost]
        [Auth(AllowLevel = IdentityLevel.Admin)]
        public JsonResult EditDo()
        {
            var id = GetParam<int>("inputId");
            var userName = GetParam<string>("inputName");
            var passWord = GetParam<string>("inputPass");
            var email = GetParam<string>("inputEmail");
            var mobile = GetParam<string>("inputMobile");
            var status = GetParam<string>("inputStatus") == "on" ? 1 : 0;
            var level = GetParam<int>("selectLevel") == 2 ? 2 : 1;
            Request.Validation(
                new ValidationEntity { Key = id, Des = "ID不可小于零", Type = ValidationType.IdInt },
                new ValidationEntity { Key = userName, Des = "用户名不得为空", Type = ValidationType.Required },
                new ValidationEntity { Key = email, Des = "邮箱格式不正在确", Type = ValidationType.Email }
            );
            using (var db = new BaseModel())
            {
                if (db.User.Count(rs => rs.UserName == userName && rs.Id != id) > 0)
                {
                    return Tools.ReJson("该用户名已存在");
                }
                var result = db.User.FirstOrDefault(rs => rs.Id == id);
                if (result == null)
                {
                    return Tools.ReJson("获取用户信息失败");
                }
                if (!string.IsNullOrWhiteSpace(passWord))
                {
                    result.PassWord = Tools.Md5(passWord);
                }
                result.Email = email;
                result.Mobile = mobile;
                result.UserName = userName;
                result.Level = level;
                result.Status = status;
                db.User.Update(result);
                if (db.SaveChanges() <= 0)
                {
                    return Tools.ReJson("更新用户信息失败");
                }
                //如果是本人信息，则清空登录信息需要重新登录
                if (Auth.Info.Id == id)
                {
                    ClearAuth();
                }
            }
            return Tools.ReJson();
        }

        [HttpPost]
        [Auth(AllowLevel = IdentityLevel.Admin)]
        public JsonResult DelDo()
        {
            var id = GetParam<int>("id");
            Request.Validation(new ValidationEntity { Key = id, Des = "ID不可小于零", Type = ValidationType.IdInt });
            if (id == Auth.Info.Id)
            {
                return Tools.ReJson("不可以删除自已");
            }
            using (var db = new BaseModel())
            {
                var result = db.User.FirstOrDefault(rs => rs.Id == id);
                if (result == null)
                {
                    return Tools.ReJson("获取用户信息失败");
                }

                db.User.Remove(result);
                if (db.SaveChanges() <= 0)
                {
                    return Tools.ReJson("删除用户信息失败");
                }
            }

            return Tools.ReJson();
        }

    }
}