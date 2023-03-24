## 一、API 支持情况
> Abp.Mirai.Webhook模块目前仅实现了对消息和事件的接受。暂不支持通过http response对Mirai Bot发送命令(开发中)
#### 1.1 Webhook API

| 功能             | 是否支持                                                     |
| ---------------- | ------------------------------------------------------------ |
| 消息回调         | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 事件回调         | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 返回Bot指令 | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |

## 二、基本模块配置

### 2.1 模块的引用

添加 **Abp.Mirai.Webhook** 模块的 NuGet 包或者项目引用到 **Domain** 层，并在对应的模块上面添加 `[DependsOn]` 特性标签。

```csharp
[DependsOn(typeof(AbpMiraiWebhookModule))]
public class XXXDomainModule : AbpModule
{

}
```

### 2.2 模块的配置

微信模块的配置参数都存放在 `AbpMiraiWebhookOptions` 内部，开发人员只需要在启动模块的 `ConfigureService()` 方法中进行配置即可，下面是最小启动配置。

```csharp
[DependsOn (typeof (AbpMiraiWebhookModule))]
public class XXXDomainModule : AbpModule 
{
    public override void ConfigureServices (ServiceConfigurationContext context) 
    {
        Configure<AbpMiraiWebhookOptions> (op => 
        {
            // 用于和ChatBot进行验证的请求头名称
            op.AuthHeaderName = "MiraiAuth";
            // 用于和ChatBot进行验证的key, 为空则无需验证
            op.AuthHeaderKey = "1234567890";
        });
    }
}
```

配置参数，可以参考 `AbpMiraiWebhookOptions` 类型的定义，上面针对各个配置参数都有详细的注释说明。

## 三、提供的回调接口

### 3.1 支付回调接口

支付通知接口的默认路由是 `/Mirai/Notify`，当Mirai Bot收到消息或者事件之后，mirai-api-http会将消息和事件通过异步回调的方式请求 **Mirai通知接口**

> 开发人员也可以自己编写回调接口，只需要在配置的时候，参数传递自己的回调接口 URL 即可。

用户如果需要对消息或者事件进行处理，只需要实现一个或多个 `IMiraiMessageHandler` 以及 `IMiraiEventHandler` 处理器即可。当框架接受到微通知时，会触发开发人员编写的处理器，并将消息和事件传递给这些处理器。

```csharp
    public class DefaultMiraiMessageHandler : IMiraiMessageHandler
    {

        private readonly ILocalEventBus _localEventBus;
        private readonly ILogger<DefaultMiraiMessageHandler> _logger;

        public DefaultMiraiMessageHandler(ILocalEventBus localEventBus, ILogger<DefaultMiraiMessageHandler> logger)
        {
            _localEventBus = localEventBus;
            _logger = logger;
        }

        public async Task HandleAsync(MessageReceiverBase context)
        {
            Type type = context.GetType();
            // 将接受到的消息使用本地EventBus进行发布， 用户可以订阅不同种类的消息进行处理
            await _localEventBus.PublishAsync(type, context);
        }
    }
```

编写完成之后，则需要开发人员手动注入这些处理器。

```csharp
public class XXXDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IMiraiMessageHandler, DefaultMiraiMessageHandler>();
    }
}
```

Webhook 模块默认提供了参数校验处理器，各个处理器的调用顺序是按照 **注入顺序** 来的，目前暂时不支持处理器自定义排序。

## 四、服务的使用

开发人员可以通过实现消息和事件的handler对不同的消息及事件类型进行处理

### 4.1 订阅QQ好友消息
> 这是一个使用本地EventBus对单一类型的消息进行订阅的示范
```csharp
    public class FriendMessageSubscriber
        : ILocalEventHandler<FriendMessageReceiver>,
          ITransientDependency
    {
        private readonly ILogger<FriendMessageSubscriber> _logger;
        private readonly IMiraiMessageManager _messageManager;
        public FriendMessageSubscriber(ILogger<FriendMessageSubscriber> logger, IMiraiMessageManager messageManager)
        {
            _logger = logger;
            _messageManager = messageManager;
        }

        public async Task HandleEventAsync(FriendMessageReceiver eventData)
        {
            if (eventData.QQ == eventData.FriendId)
            {
                _logger.LogError("不能给自己发送消息");
                return;
            }

            await _messageManager.SendFriendMessageAsync(eventData.FriendId, new MessageChain {
                new PlainMessage("收到宝宝的消息拉！")
            }, eventData.QQ);
        }
    }
```
