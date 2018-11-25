using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Spi;
using Scheduler.Core.Entity;
using Scheduler.Quartz.Listener;
using Scheduler.SignalR;

namespace Scheduler.Quartz
{
    public class SchedulerServer
    {
        private readonly ISchedulerFactory _factory;

        private IScheduler _scheduler;

        private SchedulerExtHub<SchedulerHub> ExtHub { get; set; }

        public SchedulerServer()
        {
            if (_factory == null)
            {
                _factory = new StdSchedulerFactory();
                GetScheduler().GetAwaiter().GetResult();
            }

        }

        private async Task GetScheduler()
        {
            _scheduler = await _factory.GetScheduler();
            _scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener());
            _scheduler.ListenerManager.AddJobListener(new JobListener("JobListener"));
            _scheduler.ListenerManager.AddTriggerListener(new TriggerListener("TriggerListener"));
            await _scheduler.Start();
        }

        /// <summary>
        /// 获取CoreTab规则的运行时间列表
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<string> FireTimeList(string rule, int count)
        {
            try
            {
                var trigger = TriggerBuilder.Create().WithCronSchedule(rule).Build();
                var dataSet = TriggerUtils.ComputeFireTimes(trigger as IOperableTrigger, null, count);
                return dataSet.Select(t =>
                        TimeZoneInfo.ConvertTimeFromUtc(t.DateTime, TimeZoneInfo.Local).ToString("yyyy-MM-dd HH:mm:ss"))
                    .ToList();
            }
            catch (FormatException)
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// 获取触发器数量
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<int,int> GetJobCount()
        {
            var runCount = 0;
            var totalCount = 0;
            var jobKeys = _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
            foreach (var jobKey in jobKeys.Result)
            {
                var triggers = _scheduler.GetTriggersOfJob(jobKey);
                foreach (var trigger in triggers.Result)
                {
                    totalCount++;
                    if (_scheduler.GetTriggerState(new TriggerKey(trigger.Key.Name, trigger.Key.Group)).Result ==
                        TriggerState.Normal)
                    {
                        runCount++;
                    }
                }
            }
            return new KeyValuePair<int, int>(totalCount,runCount);
        }

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, SchedulerDetailEntity> GetJobList()
        {
            var dic = new Dictionary<int, SchedulerDetailEntity>();
            var jobKeys = _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
            foreach (var jobKey in jobKeys.Result)
            {
                var triggers = _scheduler.GetTriggersOfJob(jobKey);
                var dicTriggers = new Dictionary<string, SchedulerDetailTrigger>();
                foreach (var trigger in triggers.Result)
                {
                    var detailTrigger = new SchedulerDetailTrigger
                    {
                        Key = trigger.Key.Name,
                        Group = trigger.Key.Group,
                        Status = _scheduler.GetTriggerState(new TriggerKey(trigger.Key.Name, trigger.Key.Group)).Result.GetHashCode()
                    };
                    var finalFireTime = trigger.FinalFireTimeUtc;
                    if (finalFireTime.HasValue)
                    {
                        detailTrigger.FinalFireTime = finalFireTime.Value.DateTime.ToLocalTime();
                    }

                    var endRunTime = trigger.EndTimeUtc;
                    if (endRunTime.HasValue)
                    {
                        detailTrigger.EndRunTime = endRunTime.Value.DateTime.ToLocalTime();
                    }

                    var preRunTime = trigger.GetPreviousFireTimeUtc();
                    if (preRunTime.HasValue)
                    {
                        detailTrigger.PreRunTime = preRunTime.Value.DateTime.ToLocalTime();
                    }

                    var nextRunTime = trigger.GetNextFireTimeUtc();
                    if (nextRunTime.HasValue)
                    {
                        detailTrigger.NextRunTime = nextRunTime.Value.DateTime.ToLocalTime();
                    }
                    dicTriggers.Add(trigger.Key.Name, detailTrigger);
                }

                dic.Add(int.Parse(jobKey.Name), new SchedulerDetailEntity
                {
                    Key = jobKey.Name,
                    Group = jobKey.Group,
                    DetailTriggers = dicTriggers
                });
            }

            return dic;
        }

        /// <summary>
        /// 添加多个jobs
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="extHub"></param>
        /// <returns></returns>
        public async Task AddJobs(Dictionary<SchedulerJobEntity, SchedulerTriggerEntity> dic, SchedulerExtHub<SchedulerHub> extHub)
        {
            ExtHub = extHub;
            if (_scheduler.IsStarted)
            {
                var jobs = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>();
                foreach (var entity in dic)
                {
                    var job = GetJob(entity.Key);
                    var triggers = new List<ITrigger> { GetTrigger(entity.Value) };
                    jobs.Add(job, triggers);
                }
                await _scheduler.ScheduleJobs(jobs, true);
            }
        }

        public async Task RemoveJob(string key)
        {
            if (_scheduler.IsStarted)
            {
                if (await _scheduler.CheckExists(new JobKey(key, SchedulerJobEntity.Group)))
                {
                    await _scheduler.DeleteJob(new JobKey(key, SchedulerJobEntity.Group));
                }
            }
        }

        /// <summary>
        /// 添加一个job
        /// </summary>
        /// <param name="job"></param>
        /// <param name="trigger"></param>
        /// <returns></returns>
        public async Task AddJob(SchedulerJobEntity job, SchedulerTriggerEntity trigger)
        {
            if (_scheduler.IsStarted)
            {
                await _scheduler.ScheduleJob(GetJob(job), GetTrigger(trigger));
            }
        }

        public async Task RunJob(string key)
        {
            if (_scheduler.IsStarted)
            {
                await _scheduler.TriggerJob(new JobKey(key, SchedulerJobEntity.Group));
            }
        }

        /// <summary>
        /// 恢复触发器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task ResumeTrigger(string key)
        {
            if (_scheduler.IsStarted)
            {
                await _scheduler.ResumeTrigger(new TriggerKey(key, SchedulerTriggerEntity.Group));
            }
        }

        /// <summary>
        /// 暂停触发器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task PauseTrigger(string key)
        {
            if (_scheduler.IsStarted)
            {
                await _scheduler.PauseTrigger(new TriggerKey(key, SchedulerTriggerEntity.Group));
            }
        }


        /// <summary>
        /// 获取job
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        private IJobDetail GetJob(SchedulerJobEntity job) => JobBuilder.Create<ScheduleJob>().WithDescription(job.TaskInfo.Description)
            .SetJobData(new JobDataMap
            {
                {"SchedulerHub", ExtHub},
                {"TaskInfo", job.TaskInfo },
                {"EmailInfo",job.EmailInfo }
            })
            .WithIdentity(job.Key, SchedulerJobEntity.Group)
            .Build();

        /// <summary>
        /// 获取trigger
        /// </summary>
        /// <param name="trigger"></param>
        /// <returns></returns>
        private ITrigger GetTrigger(SchedulerTriggerEntity trigger) => TriggerBuilder.Create()
                .WithIdentity(trigger.Key, SchedulerTriggerEntity.Group)
                .WithDescription(trigger.Desc)
                .StartNow()
                .WithCronSchedule(trigger.Rule)
                .Build();
    }
}
