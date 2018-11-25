## ScheduleTask
基于Quartz.Net扩展，.Net Core开发的定时任务调度系统

### 环境
- .Net Core2.1+
- Web管理界面需支持WebSocket浏览器

	>安装.net core运行环境

	> https://www.microsoft.com/net/learn/dotnet/hello-world-tutorial#install

### 运行

1. 报警邮件配置：
```json
{
  "email": {
    "smtp": "smtp.163.com", //邮件服务器地址
    "port": 25,
    "address": "mylovefly001@163.com", //发件方地址
    "user": "",
    "pass": ""
  }
}
```

2. 运行命令：

```bash
dotnet Scheduler.Main.dll [Web管理界面运行端口] //默认5000端口
------------
dotnet Scheduler.Main.dll 6000
```

### WEB管理界面
