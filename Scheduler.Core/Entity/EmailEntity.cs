using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Core.Entity
{
    public class EmailEntity
    {
        public string Smtp { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
    }
}
