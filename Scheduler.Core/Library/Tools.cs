using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Scheduler.Core.Library
{
    public class Tools
    {
        /// <summary>
        /// 获取字符串的MD5值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5(string str)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                return BitConverter.ToString(result).Replace("-", "");
            }
        }


        /// <summary>
        /// 返回带时间的格式化字符串
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string Msg(string msg)
        {
            return $"{DateTime.Now.ToLocalTime()}：{msg}";
        }

        /// <summary>
        /// 返回统一的数据格式
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static JsonResult ReJson(object val = null)
        {
            var result = new Entity.JsonResultEntity()
            {
                Code = 0,
                Msg = "",
                Data = new JArray(),
                Time = DateTime.Now.ToLocalTime()
            };
            if (val != null)
            {
                if (val is string)
                {
                    result.Code = 1;
                    result.Msg = val.ToString();
                }
                else
                {
                    result.Data = val;
                }
            }
            return new JsonResult(result);
        }
    }
}
