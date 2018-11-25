using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Scheduler.Core.Library;

namespace Scheduler.Quartz.Listener
{
   public class SchedulerListener:ISchedulerListener
    {
        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default(CancellationToken))
        {
            var msg = Tools.Msg($"scheduler:{jobDetail.Key.Name} 任务添加完成(JobAdded)");
            return Console.Out.WriteLineAsync(msg);
        }

        public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            var msg = Tools.Msg($"scheduler:{jobKey.Name} 任务删除完成(JobDeleted)");
            return Console.Out.WriteLineAsync(msg);
        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            var msg = Tools.Msg($"scheduler:{jobKey.Name} 任务暂停成功(JobPaused)");
            return Console.Out.WriteLineAsync(msg);
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            var msg = Tools.Msg($"scheduler:{jobKey.Name} 任务恢复运行完成(JobResumed)");
            return Console.Out.WriteLineAsync(msg);
        }

        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default(CancellationToken))
        {
            var msg = Tools.Msg($"scheduler:{trigger.Key.Name} 触发器载入成功(JobScheduled)");
            return Console.Out.WriteLineAsync(msg);
        }

        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            var msg = Tools.Msg($"scheduler:{triggerKey.Name} 触发器卸载完成(JobUnscheduled)");
            return Console.Out.WriteLineAsync(msg);
        }

        /// <summary>
        /// 调度器发生错误
        /// </summary>
        /// <param name="str"></param>
        /// <param name="cause"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SchedulerError(string str, SchedulerException cause, CancellationToken cancellationToken = default(CancellationToken))
        {
            var msg = Tools.Msg($"scheduler:任务调度器错误,{str}");
            return Console.Out.WriteLineAsync(msg);
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task SchedulerShutdown(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 任务调度器启动完成
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SchedulerStarted(CancellationToken cancellationToken = default(CancellationToken))
        {
            var msg = Tools.Msg($"scheduler:任务调度器启动完成");
            return Console.Out.WriteLineAsync(msg);
        }

        /// <summary>
        /// 任务调度器启动
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SchedulerStarting(CancellationToken cancellationToken = default(CancellationToken))
        {
            var msg = Tools.Msg($"scheduler:任务调度器启动中...");
            return Console.Out.WriteLineAsync(msg);
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default(CancellationToken))
        {
            var msg = Tools.Msg($"scheduler:触发器运行一次完成");
            return Console.Out.WriteLineAsync(msg);
        }

        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            var msg = Tools.Msg($"scheduler:触发器暂停完成");
            return Console.Out.WriteLineAsync(msg);
        }

        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            var msg = Tools.Msg($"scheduler:触发器恢复运行完成");
            return Console.Out.WriteLineAsync(msg);
        }

        public Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
