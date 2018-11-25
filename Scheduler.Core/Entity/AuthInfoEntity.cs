using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Core.Entity
{
    /// <summary>
    /// 权限验证信息
    /// </summary>
    public class AuthInfoEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户等级：1=普通用户|2=管理员
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 用户状态：0=禁用|1=启用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
    }
}
