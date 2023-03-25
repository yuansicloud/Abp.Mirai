using Newtonsoft.Json;
using YSCloud.Abp.Mirai.Common.Data.Messages;
using YSCloud.Abp.Mirai.Common.Data.Shared;

namespace YSCloud.Abp.Mirai.Common.Data.Messages.Receivers;

/// <summary>
/// 群消息接收器s
/// </summary>
public record GroupMessageReceiver : MessageReceiverBase
{
    /// <summary>
    /// 消息接收器类型
    /// </summary>
    [JsonProperty("sender")] public Member Sender { get; set; }

    /// <summary>
    /// 群号
    /// </summary>
    [JsonIgnore]
    public string GroupId => Sender.Group.Id;

    /// <summary>
    /// 群名称
    /// </summary>
    [JsonIgnore]
    public string GroupName => Sender.Group.Name;

    /// <summary>
    /// bot在群内的权限
    /// </summary>
    [JsonIgnore]
    public Permissions BotPermission => Sender.Group.Permission;

    /// <summary>
    /// 消息接收器类型
    /// </summary>
    public override MessageReceivers Type { get; set; } = MessageReceivers.Group;
}