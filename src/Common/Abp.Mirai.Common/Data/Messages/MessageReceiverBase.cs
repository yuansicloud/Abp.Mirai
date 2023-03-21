using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Abp.Mirai.Common.Data.Messages;

/// <summary>
/// 消息接收器基类
/// </summary>
public record MessageReceiverBase
{
    /// <summary>
    /// 消息接收器类型
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public virtual MessageReceivers Type { get; set; }

    /// <summary>
    /// 接收消息的QQ号
    /// </summary>
    [JsonIgnore]
    public virtual string QQ { get; set; }

    /// <summary>
    /// 接受到的消息链
    /// </summary>
    [JsonProperty("messageChain")] public MessageChain MessageChain { get; set; }
}