using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Scheduler.Core.Attribute;
using Scheduler.Core.Entity;
using Scheduler.Core.Enum;
using Scheduler.Core.Library;
using Scheduler.Model;
using Scheduler.Quartz;

namespace Scheduler.Main.Controllers
{
    [Auth(AllowLevel = IdentityLevel.All)]
    public class IndexController : BaseController
    {
        private readonly SchedulerServer _schedulerServer;

        public IndexController(SchedulerServer schedulerServer, IConfiguration configuration)
        {
            _schedulerServer = schedulerServer;
        }


        [HttpGet]
        public IActionResult Index()
        {
            using (var db = new BaseModel())
            {
                //当前总任务数量
                var enableTask = 0;
                var disableTask = 0;
                foreach (var taskModel in db.Task)
                {
                    if (taskModel.Status == 1)
                    {
                        enableTask++;
                    }
                    else
                    {
                        disableTask++;
                    }
                }

                ViewBag.EnableTask = enableTask;
                ViewBag.DisableTask = disableTask;
            }

            //运行中的任务
            var triggerResult = _schedulerServer.GetJobCount();
            ViewBag.TaskRunCount = triggerResult.Value;
            ViewBag.TaskTotalCount = triggerResult.Key;

            return View();
        }

        /// <summary>
        /// 获取job列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetJobList()
        {
            return Tools.ReJson(_schedulerServer.GetJobList());
        }

        /// <summary>
        /// 获取一些统计数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetChartList()
        {
            using (var db = new BaseModel())
            {
                var montyStartDay = DateTime.Now.AddDays(1 - DateTime.Now.Day);
                var montyLastDay = montyStartDay.AddMonths(1).AddDays(-1);
                var query = from loggerModel in db.Logger
                    join taskModel in db.Task on loggerModel.TaskId equals taskModel.Id into t1
                    from t1Model in t1.DefaultIfEmpty()
                    select new
                    {
                        runTime = loggerModel.RunTime,
                        runStatus = loggerModel.Status,
                        createdTime = loggerModel.CreatedTime,
                        name = t1Model.Name,
                        taskId = loggerModel.TaskId
                    };
                return Tools.ReJson(query.Where(rs => rs.createdTime >= montyStartDay && rs.createdTime <= montyLastDay)
                    .ToArray());
            }

        }
    }
}