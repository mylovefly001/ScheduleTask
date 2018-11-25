using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Core.Entity
{
    /// <summary>
    /// 返回的Json
    /// </summary>
    public class JsonResultEntity
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Time { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
    }
}
