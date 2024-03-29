﻿using YSCloud.Abp.Mirai.Common.Data.Messages;

namespace YSCloud.Abp.Mirai.Common.Handlers
{
    /// <summary>
    /// 定义了Mirai消息回调处理器。
    /// </summary>
    public interface IMiraiMessageHandler
    {
        Task HandleAsync(MessageReceiverBase context);
    }
}
