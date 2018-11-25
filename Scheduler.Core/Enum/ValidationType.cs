using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Core.Enum
{
    /// <summary>
    /// 验证字段类型
    /// </summary>
    public enum ValidationType
    {
        /// <summary>
        /// 必填类型
        /// </summary>
        Required = 1,

        /// <summary>
        /// 邮箱类型
        /// </summary>
        Email = 2,

        /// <summary>
        /// 大于0的整数
        /// </summary>
        IdInt = 3,

        /// <summary>
        /// 整数
        /// </summary>
        Integer = 4
    }
}
