using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Core.Entity
{
    /// <summary>
    /// 调试工作实体
    /// </summary>
    public class SchedulerJobEntity
    {
        /// <summary>
        /// 工作KEY
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 工作组
        /// </summary>
        public const string Group = "1";
        /// <summary>
        /// 任务信息
        /// </summary>
        public TaskInfoEntity TaskInfo { get; set; }
        /// <summary>
        /// 邮件设置信息
        /// </summary>
        public EmailEntity EmailInfo { get; set; }
    }
}
