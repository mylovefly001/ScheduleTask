using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Scheduler.Core.Library;

namespace Scheduler.Main.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 每页数据条数
        /// </summary>
        protected const int PageNum = 20;



        /// <summary>
        /// 清除登录信息
        /// </summary>
        protected void ClearAuth()
        {
            HttpContext.Session.Remove(Auth.Key);
            Auth.Info = null;
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="key">参数名称</param>
        /// <param name="def">参数默认值</param>
        /// <returns></returns>
        protected T GetParam<T>(string key, object def = null)
        {
            var val = default(T);
            try
            {
                if (def != null)
                {
                    val = (T) Convert.ChangeType(def, typeof(T));
                }

                switch (Request.Method.ToUpper())
                {
                    case "GET" when Request.Query.ContainsKey(key):
                    {
                        var v = Request.Query[key].FirstOrDefault();
                        val = (T) Convert.ChangeType(v, typeof(T));
                        break;
                    }
                    case "POST" when Request.Form.ContainsKey(key):
                    {
                        var v = Request.Form[key].FirstOrDefault();
                        val = (T) Convert.ChangeType(v, typeof(T));
                        break;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return val;
        }
        
    }
}