using Abp.Mirai.Common.Data.Messages;
using Abp.Mirai.Common.Data.Messages.Concretes;
using Abp.Mirai.Common.Data.Messages.Receivers;
using Abp.Mirai.Common.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace Abp.Mirai.Demo.Subscribers
{
    public class FriendMessageSubscriber
        : ILocalEventHandler<FriendMessageReceiver>,
          ITransientDependency
    {
        private readonly ILogger<FriendMessageSubscriber> _logger;
        private readonly IMessageManager _messageManager;
        public FriendMessageSubscriber(ILogger<FriendMessageSubscriber> logger, IMessageManager messageManager)
        {
            _logger = logger;
            _messageManager = messageManager;
        }

        [UnitOfWork]
        public async Task HandleEventAsync(FriendMessageReceiver eventData)
        {
            //TODO: your code that does something on the event
            _logger.LogInformation(eventData.FriendName + "发送了一条消息");
            await _messageManager.SendFriendMessageAsync(eventData.FriendId, new MessageChain {
                new PlainMessage("收到宝宝的消息拉！")
            }, eventData.QQ);
        }
    }
}
