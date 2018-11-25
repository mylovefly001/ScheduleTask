using System;
using System.Collections.Generic;
using System.Text;
using Scheduler.Core.Enum;

namespace Scheduler.Core.Factory
{
    public class UtilException:ApplicationException
    {
        public string Msg { get; }

        public ExceptionType Code { get; }
        

        public UtilException(string msg):base(msg)
        {
            Msg = msg;
        }

        public UtilException(string msg, ExceptionType code) : base(msg)
        {
            Msg = msg;
            Code = code;
        }
    }
}
