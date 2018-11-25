using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Scheduler.Core.Factory;
using Scheduler.Core.Library;

namespace Scheduler.Core.Filter
{
    public class ExceptionFilter: IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = 200;
            if (context.Exception is UtilException exception)
            {
                context.Result = Tools.ReJson(exception.Msg);
                Console.WriteLine(exception.Msg);
            }
        }
    }
}
