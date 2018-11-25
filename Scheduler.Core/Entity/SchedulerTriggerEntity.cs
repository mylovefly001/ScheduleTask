using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Core.Entity
{
    /// <summary>
    /// 调试触发器实体
    /// </summary>
    public class SchedulerTriggerEntity
    {
        /// <summary>
        /// 触发器Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 触发器组
        /// </summary>
        public const string Group = "1";

        /// <summary>
        /// 触发器描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 触发器规则
        /// </summary>
        public string Rule { get; set; }
    }
}
