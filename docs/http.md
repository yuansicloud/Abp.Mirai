# http模块

## 一、API 支持情况

### 1.1 自动登录

| 功能         | 是否支持                                                     |
| ------------ | ------------------------------------------------------------ |
| 缓存和绑定session | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |

### 1.2 消息管理

| 功能                   | 是否支持                                                     |
| ---------------------- | ------------------------------------------------------------ |
| 不同消息类型的发送 | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |

### 1.3 群管理

| 功能         | 是否支持                                                     |
| ------------ | ------------------------------------------------------------ |
| QQ群管理功能实现 | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |

### 1.4 账号管理

| 功能         | 是否支持                                                     |
| ------------ | ------------------------------------------------------------ |
| 获取登录账号相关信息 | ![Support](https://img.shields.io/badge/-开发中-yellow.svg) |

### 1.5 请求管理

| 功能         | 是否支持                                                     |
| ------------ | ------------------------------------------------------------ |
| 好友申请入请申请等 | ![Support](https://img.shields.io/badge/-开发中-yellow.svg) |

### 1.6 文件管理

| 功能         | 是否支持                                                     |
| ------------ | ------------------------------------------------------------ |
| 获取群文件列表、文件上传 | ![Support](https://img.shields.io/badge/-开发中-yellow.svg) |


## 二、基本模块配置

### 2.1 模块的引用

添加 **Abp.Mirai.Http** 模块的 NuGet 包或者项目引用到 **Domain** 层，并在对应的模块上面添加 `[DependsOn]` 特性标签。

```csharp
[DependsOn(typeof(AbpMiraiHttpModule))]
public class XXXDomainModule : AbpModule
{

}
```

### 2.2 模块的配置

微信模块的配置参数都存放在 `AbpMiraiHttpOptions` 内部，开发人员只需要在启动模块的 `ConfigureService()` 方法中进行配置即可，下面是最小启动配置。

```csharp
[DependsOn(typeof(AbpMiraiHttpModule))]
public class XXXDomainModule : AbpModule 
{
    public override void ConfigureServices(ServiceConfigurationContext context) 
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpMiraiHttpOptions>(op =>
        {
            // Mirai Bot IP 地址
            op.Host = configuration["AbpMiraiHttpOptions:Host"];
            // MiraiBot 端口
            op.Port = configuration["AbpMiraiHttpOptions:Port"];
            // MiraiBot 验证码
            op.VerifyKey = configuration["AbpMiraiHttpOptions:VerifyKey"];
            // 使用Http进行轮询获取消息的频率
            op.PollingRate = Convert.ToInt32(configuration["AbpMiraiHttpOptions:PollingRate"]);
        });
    }
}
```

进行上述配置以后，你的项目就集成了Mirai Http功能。现在，你可以在任意地方注入服务类，通过服务类快捷地调用mirai-api-http所提供的 API 接口服务进行消息发送，QQ群管理等。

## 三、默认启用的接口

// TODO。

## 四、服务的使用

>所有的服务都是基于Abp.Mirai.Common中的interface进行实现。开发人员可以自定义实现服务

### 4.1 自动登录

该模块会自动在分布式缓存(DistributedCache)中储存用到的Session信息。开发人员可以自行接入Redis

### 4.2 消息管理

开发人员如果需要使用消息管理服务，只需要注入 `IMiraiMessageManager` 类型即可，该类型的生命周期为 **瞬时对象** 。

### 4.3 群管理

开发人员如果需要使用消息管理服务，只需要注入 `IMiraiGroupManager` 类型即可，该类型的生命周期为 **瞬时对象** 。
