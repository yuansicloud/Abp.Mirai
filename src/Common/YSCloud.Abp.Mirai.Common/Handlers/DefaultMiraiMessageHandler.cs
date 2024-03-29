﻿using Microsoft.Extensions.Logging;
using Volo.Abp.EventBus.Local;
using YSCloud.Abp.Mirai.Common.Data.Messages;

namespace YSCloud.Abp.Mirai.Common.Handlers
{
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
            await _localEventBus.PublishAsync(type, context);
        }
    }
}
