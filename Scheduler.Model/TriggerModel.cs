using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Scheduler.Model
{
    [Table("trigger")]
    public class TriggerModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 创建者用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 触发器名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 触发器值
        /// </summary>
        public string Value { get; set; }
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
