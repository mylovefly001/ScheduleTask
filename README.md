# 计划任务调度系统

[![star](https://gitee.com/otman/scheduler/badge/star.svg?theme=white)](https://gitee.com/otman/scheduler/stargazers)

#### 项目介绍
计划任务调度系统

#### 软件架构
基于Quartz.Net扩展，.Net Core开发的定时任务调度系统


#### 安装教程


- .Net Core2.1+

- Web管理界面需支持WebSocket浏览器

- 安装.net core运行环境

	> https://www.microsoft.com/net/learn/dotnet/hello-world-tutorial#install


#### 使用说明


1. 报警邮件配置：
```json
{
  "email": {
    "smtp": "smtp.163.com", //邮件服务器地址
    "port": 25,
    "address": "发件人@163.com", //发件方地址
    "user": "",
    "pass": ""
  }
}
```

2. 运行命令：

```bash
dotnet Scheduler.Main.dll [Web管理界面运行端口] //默认5000端口，不带端口只能本地localhost访问
------------
dotnet Scheduler.Main.dll 6000
```
### 下载

>https://gitee.com/otman/scheduler/releases

### WEB管理界面

>默认用户名：admin 密码：123

![输入图片说明](https://images.gitee.com/uploads/images/2018/1130/181506_4649afa8_66542.png "288C3EFD-9A04-420d-BCF6-803DB449C158.png")

#### 参与贡献

1. Fork 本项目
2. 新建 Feat_xxx 分支
3. 提交代码
4. 新建 Pull Request
