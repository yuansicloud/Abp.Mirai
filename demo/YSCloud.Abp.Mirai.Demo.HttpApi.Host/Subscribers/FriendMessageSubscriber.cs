using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;
using YSCloud.Abp.Mirai.Common.Data.Messages;
using YSCloud.Abp.Mirai.Common.Data.Messages.Concretes;
using YSCloud.Abp.Mirai.Common.Data.Messages.Receivers;
using YSCloud.Abp.Mirai.Common.Services;

namespace YSCloud.Abp.Mirai.Demo.HttpApi.Host.Subscribers
{
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

        [UnitOfWork]
        public async Task HandleEventAsync(FriendMessageReceiver eventData)
        {
            //TODO: your code that does something on the event
            //_logger.LogInformation(eventData.FriendName + "发送了一条消息");

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
}
