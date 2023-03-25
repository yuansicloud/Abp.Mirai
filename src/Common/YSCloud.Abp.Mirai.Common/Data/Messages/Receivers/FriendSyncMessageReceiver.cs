using Newtonsoft.Json;
using YSCloud.Abp.Mirai.Common.Data.Messages;
using YSCloud.Abp.Mirai.Common.Data.Shared;

namespace YSCloud.Abp.Mirai.Common.Data.Messages.Receivers
{

    /// <summary>
    /// 好友同步消息接收器
    /// </summary>
    public record FriendSyncMessageReceiver : MessageReceiverBase
    {

        /// <summary>
        /// 消息接收器类型
        /// </summary>
        public override MessageReceivers Type { get; set; } = MessageReceivers.FriendSync;

        /// <summary>
        /// 好友信息
        /// </summary>
        [JsonProperty("subject")] public Friend TargetFriend { get; set; }

    }
}
