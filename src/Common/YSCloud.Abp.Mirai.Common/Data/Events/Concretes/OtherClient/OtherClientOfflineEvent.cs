﻿using Newtonsoft.Json;
using YSCloud.Abp.Mirai.Common.Data.Events;

namespace YSCloud.Abp.Mirai.Common.Data.Events.Concretes.OtherClient;

/// <summary>
/// 其它客户端离线
/// </summary>
public record OtherClientOfflineEvent : EventBase
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public override Events Type { get; set; } = Events.OtherClientOffline;

    /// <summary>
    /// 啥客户端
    /// </summary>
    [JsonProperty("client")]
    public Shared.OtherClient Client { get; set; }
}