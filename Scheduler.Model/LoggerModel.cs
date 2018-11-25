using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Scheduler.Model
{
    [Table("logger")]
    public class LoggerModel
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 运行时间,单位：秒
        /// </summary>
        public double RunTime { get; set; }
        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// 任务结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 任务状态：0=请求失败，1=请求成功
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
