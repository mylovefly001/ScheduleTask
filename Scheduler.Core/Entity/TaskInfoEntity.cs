using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Core.Entity
{
    public class TaskInfoEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 触发器ID
        /// </summary>
        public int TriggerId { get; set; }
        /// <summary>
        /// 触发器值
        /// </summary>
        public string TriggerValue { get; set; }

        /// <summary>
        /// 触发器说明
        /// </summary>
        public string TriggerDesc { get; set; }

        /// <summary>
        /// 任务类型：1=命令行，2=CURL
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 任务类型值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 任务状态：0=未审核，1=已审核
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 任务组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }
    }
}
