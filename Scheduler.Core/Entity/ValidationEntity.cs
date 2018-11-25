using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Core.Entity
{
    /// <summary>
    /// 验证实体
    /// </summary>
    public class ValidationEntity
    {
        /// <summary>
        /// 值
        /// </summary>
        public object Key { get; set; }

        /// <summary>
        /// 验证类型
        /// </summary>
        public Enum.ValidationType Type { get; set; }

        /// <summary>
        /// 验证返回说明
        /// </summary>
        public string Des { get; set; }
    }
}
