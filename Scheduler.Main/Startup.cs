using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Core.Entity;
using Scheduler.Core.Filter;
using Scheduler.Model;
using Scheduler.SignalR;

namespace Scheduler.Main
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => { options.Filters.Add<ExceptionFilter>(); })
                .AddJsonOptions(options => { options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSession(session => { session.IdleTimeout = TimeSpan.FromMinutes(30); });
            services.AddSignalR();
            services.AddSingleton<SchedulerExtHub<SchedulerHub>>();
            services.AddSingleton<Quartz.SchedulerServer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes => { routes.MapRoute(name: "default", template: "{controller=Index}/{action=Index}"); });
            app.UseSignalR(routes => { routes.MapHub<SchedulerHub>("/scheduler"); });
            var extHub = app.ApplicationServices.GetService<SchedulerExtHub<SchedulerHub>>();
            app.ApplicationServices.GetService<Quartz.SchedulerServer>().AddJobs(GetTaskList(), extHub).GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// 初次运行时读取所有可运行任务列表载入任务调度器
        /// </summary>
        /// <returns></returns>
        private Dictionary<SchedulerJobEntity, SchedulerTriggerEntity> GetTaskList()
        {
            var emailEntity = new EmailEntity();
            Configuration.GetSection("email").Bind(emailEntity);
            var dic = new Dictionary<SchedulerJobEntity, SchedulerTriggerEntity>();
            using (var db = new BaseModel())
            {
                var query = from taskModel in db.Task
                    join userModel in db.User on taskModel.UserId equals userModel.Id into t1
                    from t1Model in t1.DefaultIfEmpty()
                    join groupModel in db.Group on taskModel.GroupId equals groupModel.Id into t2
                    from t2Model in t2.DefaultIfEmpty()
                    select new TaskInfoEntity
                    {
                        Id = taskModel.Id,
                        Name = taskModel.Name,
                        Description = taskModel.Description,
                        UserId = taskModel.UserId,
                        UserName = t1Model.UserName,
                        UserEmail = t1Model.Email,
                        TriggerId = taskModel.TriggerId,
                        TriggerValue = taskModel.TriggerValue,
                        TriggerDesc = taskModel.TriggerDesc,
                        Type = taskModel.Type,
                        Value = taskModel.Value,
                        Status = taskModel.Status,
                        CreatedTime = taskModel.CreatedTime,
                        GroupName = t2Model.Name
                    };

                foreach (var infoEntity in query.Where(rs => rs.Status == 1))
                {
                    dic.Add(
                        new SchedulerJobEntity
                        {
                            Key = infoEntity.Id.ToString(),
                            TaskInfo = infoEntity,
                            EmailInfo = emailEntity
                        },
                        new SchedulerTriggerEntity
                        {
                            Key = infoEntity.Id + "-" + infoEntity.TriggerId,
                            Desc = infoEntity.TriggerDesc,
                            Rule = infoEntity.TriggerValue
                        }
                    );
                }
            }

            return dic;
        }
    }
}
