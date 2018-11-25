using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Scheduler.Core.Entity;
using Scheduler.Core.Enum;
using Scheduler.Core.Factory;

namespace Scheduler.Core.Library
{
    public static class Helper
    {
        /// <summary>
        /// 判断是否是Ajax请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjax(this HttpRequest request)
        {
            var result = false;
            if (request.Headers.ContainsKey("x-requested-with"))
            {
                result = request.Headers["x-requested-with"] == "XMLHttpRequest";
            }
            return result;
        }

        /// <summary>
        /// 字段验证
        /// </summary>
        /// <param name="request"></param>
        /// <param name="validationEntities"></param>
        public static void Validation(this HttpRequest request,params ValidationEntity[] validationEntities)
        {
            foreach (var entity in validationEntities)
            {
                var result = true;
                switch (entity.Type)
                {
                    case ValidationType.Required:
                        if (string.IsNullOrWhiteSpace(entity.Key.ToString()))
                        {
                            result = false;
                        }

                        break;
                    case ValidationType.Email:
                        var r = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                        if (!r.Match(entity.Key.ToString()).Success)
                        {
                            result = false;
                        }

                        break;
                    case ValidationType.IdInt:
                        if (int.Parse(entity.Key.ToString()) <= 0)
                        {
                            result = false;
                        }
                        break;
                    case ValidationType.Integer:
                        if (!int.TryParse(entity.Key.ToString(), out _))
                        {
                            result = false;
                        }
                        break;
                }

                if (!result)
                {
                    throw new UtilException(entity.Des, ExceptionType.General);
                }
            }
        }
    }
}
