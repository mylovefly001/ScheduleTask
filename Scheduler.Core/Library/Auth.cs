using System;
using System.Collections.Generic;
using System.Text;
using Scheduler.Core.Entity;

namespace Scheduler.Core.Library
{
    public static class Auth
    {
        /// <summary>
        /// Session的KEY
        /// </summary>
        public const string Key = "auth";

        /// <summary>
        /// 是否是管理员
        /// </summary>
        public static bool IsAdmin { get; set; }



        /// <summary>
        /// 登录的用户信息
        /// </summary>
        public static AuthInfoEntity Info { get; set; }
    }
}
