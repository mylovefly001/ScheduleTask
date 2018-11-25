using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Quartz;
using Scheduler.Core.Library;

namespace Scheduler.Quartz.Listener
{
    public class JobListener:IJobListener
    {
        public JobListener(string name)
        {
            Name = name;
        }

        public string Name { get; }

        /// <summary>
        /// 如果之前trigger监听器中VetoJobExecution返回的是true，则执行此方法，其它方法皆不执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task JobExecutionVetoed(IJobExecutionContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var msg = Tools.Msg($"job:{context.JobDetail.Key.Name} 被否决了未执行(JobExecutionVetoed)");
            //return Console.Out.WriteLineAsync(msg);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 任务执行之前先执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task JobToBeExecuted(IJobExecutionContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var jobData = context.MergedJobDataMap;
            if (jobData.Get("SchedulerHub") is SignalR.SchedulerExtHub<SignalR.SchedulerHub> schedulerHub)
            {
                schedulerHub.SendMessage(new JObject
                {
                    ["type"] = 2,
                    ["jobKey"] = context.JobDetail.Key.Name,
                    ["triggerKey"] = context.Trigger.Key.Name,
                    ["percent"] = 50
                }.ToString()).GetAwaiter().GetResult();
            }
            var msg = Tools.Msg($"job:{context.JobDetail.Key.Name} 即将执行(JobToBeExecuted)");
            return Console.Out.WriteLineAsync(msg);
        }

        /// <summary>
        /// 任务执行完成后执行,jobException如果它不为空则说明任务在执行过程中出现了异常
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jobException"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var jobData = context.MergedJobDataMap;
            if (jobData.Get("SchedulerHub") is SignalR.SchedulerExtHub<SignalR.SchedulerHub> schedulerHub)
            {
                schedulerHub.SendMessage(new JObject
                {
                    ["type"] = 2,
                    ["jobKey"] = context.JobDetail.Key.Name,
                    ["triggerKey"] = context.Trigger.Key.Name,
                    ["percent"] = 80
                }.ToString()).GetAwaiter().GetResult();
            }
            var msg = Tools.Msg($"job:{context.JobDetail.Key.Name} 已被执行(JobWasExecuted)");
            return Console.Out.WriteLineAsync(msg);
        }
    }
}
