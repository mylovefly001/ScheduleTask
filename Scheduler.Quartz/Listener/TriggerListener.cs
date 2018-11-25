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
    public class TriggerListener:ITriggerListener
    {
        public TriggerListener(string name)
        {
            Name = name;
        }

        public string Name { get; }

        /// <summary>
        /// 任务完成时触发
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="triggerInstructionCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default(CancellationToken))
        {
            var jobData = context.MergedJobDataMap;
            if (jobData.Get("SchedulerHub") is SignalR.SchedulerExtHub<SignalR.SchedulerHub> schedulerHub)
            {
                schedulerHub.SendMessage(new JObject
                {
                    ["type"] = 2,
                    ["jobKey"] = context.JobDetail.Key.Name,
                    ["triggerKey"] = context.Trigger.Key.Name,
                    ["percent"] = 100
                }.ToString()).GetAwaiter().GetResult();
            }
            var msg = Tools.Msg($"trigger:{trigger.Key.Name} 触发完成(TriggerComplete)");
            return Console.Out.WriteLineAsync(msg);
        }

        /// <summary>
        /// 触发器即将被触发
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task TriggerFired(ITrigger trigger, IJobExecutionContext context,
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
                    ["percent"] = 10
                }.ToString()).GetAwaiter().GetResult();
            }

            var msg = Tools.Msg($"trigger:{trigger.Key.Name} 即将触发(TriggerFired)");
            return Console.Out.WriteLineAsync(msg);
        }

        /// <summary>
        /// 触发器被错过时执行，比如线程池中任务都在跑，没有空余线程执行job
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default(CancellationToken))
        {
            var msg = Tools.Msg($"trigger:{trigger.Key.Name} 错过了触发时间(TriggerMisfired)");
            return Console.Out.WriteLineAsync(msg);
        }

        /// <summary>
        /// 触发器触发，关联的job即将执行，如果返回true则不执行job
        /// </summary>
        /// <param name="trigger"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var jobData = context.MergedJobDataMap;
            if (jobData.Get("SchedulerHub") is SignalR.SchedulerExtHub<SignalR.SchedulerHub> schedulerHub)
            {
                schedulerHub.SendMessage(new JObject
                {
                    ["type"] = 2,
                    ["jobKey"] = context.JobDetail.Key.Name,
                    ["triggerKey"] = context.Trigger.Key.Name,
                    ["percent"] = 30
                }.ToString()).GetAwaiter().GetResult();
            }
            var msg = Tools.Msg($"trigger:{trigger.Key.Name} 被触发(VetoJobExecution)");
            Console.Out.WriteLineAsync(msg);
            return Task.FromResult(false);
        }
    }
}
