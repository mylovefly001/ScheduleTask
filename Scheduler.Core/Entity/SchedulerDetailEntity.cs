using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Core.Entity
{

    public class SchedulerDetailTrigger
    {
        public string Key { get; set; }
        public string Group { get; set; }
        public int Status { get; set; }
        public DateTime? StartRunTime { get; set; }
        public DateTime? PreRunTime { get; set; }
        public DateTime? NextRunTime { get; set; }
        public DateTime? EndRunTime { get; set; }
        public DateTime? FinalFireTime { get; set; }
    }

    public class SchedulerDetailEntity
    {
        /// <summary>
        /// 任务组
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 触发器详情
        /// </summary>
        public Dictionary<string, SchedulerDetailTrigger> DetailTriggers { get; set; }
    }
}
