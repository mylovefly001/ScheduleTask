using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Scheduler.Model
{
    public class BaseModel:DbContext
    {
        public DbSet<GroupModel> Group { get; set; }
        public DbSet<LoggerModel> Logger { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DbSet<TriggerModel> Trigger { get; set; }
        public DbSet<TaskModel> Task { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContext)
        {
            dbContext.UseSqlite($"Data Source=db.s3db");
        }
    }
}
