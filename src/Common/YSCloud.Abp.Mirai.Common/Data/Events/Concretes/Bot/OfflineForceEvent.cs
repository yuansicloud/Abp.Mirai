﻿using Newtonsoft.Json;
using YSCloud.Abp.Mirai.Common.Data.Events;

namespace YSCloud.Abp.Mirai.Common.Data.Events.Concretes.Bot;

/// <summary>
/// Bot被挤下线
/// </summary>
public record OfflineForceEvent : EventBase
{
    /// <summary>
    /// Bit的QQ号
    /// </summary>
    [JsonProperty("qq")] public string QQ { get; private set; }

    /// <summary>
    /// 事件类型
    /// </summary>
    public override Events Type { get; set; } = Events.OfflineForce;
}