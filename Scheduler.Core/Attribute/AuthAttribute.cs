using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Scheduler.Core.Entity;
using Scheduler.Core.Enum;
using Scheduler.Core.Factory;
using Scheduler.Core.Library;

namespace Scheduler.Core.Attribute
{
    public class AuthAttribute : System.Attribute, IActionFilter
    {
        /// <summary>
        /// 需要执行操作的用户等级
        /// </summary>
        public IdentityLevel AllowLevel { get; set; }


        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// <summary>
        /// Action之前执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Session.Keys.Contains(Auth.Key))
            {
                if (!context.HttpContext.Request.IsAjax())
                {
                    context.Result = new RedirectResult("/Login");
                }
                else
                {
                    throw new UtilException("用户没有登录", ExceptionType.General);
                }

            }
            else
            {
                if (Auth.Info == null)
                {
                    var result = context.HttpContext.Session.GetString(Auth.Key);
                    Auth.Info = JsonConvert.DeserializeObject<AuthInfoEntity>(result);
                    Auth.IsAdmin = Auth.Info.Level == IdentityLevel.Admin.GetHashCode();
                }

                if (AllowLevel != IdentityLevel.All && AllowLevel.GetHashCode() != Auth.Info.Level)
                {
                    throw new UtilException("没有权限执行此操作", ExceptionType.General);
                }
            }

        }
    }
}
