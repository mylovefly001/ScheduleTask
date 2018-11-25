using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Scheduler.Model
{
    [Table("task")]
    public class TaskModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 所属任务组
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
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
        /// 创建时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedTime { get; set; }
    }
}
