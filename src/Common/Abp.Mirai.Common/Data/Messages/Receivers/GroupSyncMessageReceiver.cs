using Abp.Mirai.Common.Data.Shared;
using Newtonsoft.Json;

namespace Abp.Mirai.Common.Data.Messages.Receivers
{

    /// <summary>
    /// 群同步消息接收器
    /// </summary>
    public record GroupSyncMessageReceiver : MessageReceiverBase
    {

        /// <summary>
        /// 消息接收器类型
        /// </summary>
        public override MessageReceivers Type { get; set; } = MessageReceivers.GroupSync;

        /// <summary>
        /// 群信息
        /// </summary>
        [JsonProperty("subject")] public Group TargetGroup { get; set; }

    }
}
