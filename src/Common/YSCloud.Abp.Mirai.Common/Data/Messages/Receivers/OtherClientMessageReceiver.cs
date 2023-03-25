using Newtonsoft.Json;
using YSCloud.Abp.Mirai.Common.Data.Messages;
using YSCloud.Abp.Mirai.Common.Data.Shared;

namespace YSCloud.Abp.Mirai.Common.Data.Messages.Receivers;

/// <summary>
/// 其他客户端消息接收器
/// </summary>
public record OtherClientMessageReceiver : MessageReceiverBase
{
    /// <summary>
    /// 消息接收器类型
    /// </summary>
    public override MessageReceivers Type { get; set; } = MessageReceivers.OtherClient;

    /// <summary>
    /// 发送者
    /// </summary>
    [JsonProperty("sender")] public OtherClient Sender { get; set; }
}