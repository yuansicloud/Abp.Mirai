﻿using Newtonsoft.Json;

namespace YSCloud.Abp.Mirai.Common.Data.Events.Concretes.Bot;

/// <summary>
///     Bot主动离线
/// </summary>
public record OfflineEvent : EventBase
{
    /// <summary>
    /// Bot的QQ号
    /// </summary>
    [JsonProperty("qq")] public string QQ { get; private set; }

    /// <summary>
    /// 事件类型
    /// </summary>
    public override Events Type { get; set; } = Events.Offline;
}