﻿using Newtonsoft.Json;
using YSCloud.Abp.Mirai.Common.Data.Events;
using YSCloud.Abp.Mirai.Common.Data.Shared;

namespace YSCloud.Abp.Mirai.Common.Data.Events.Concretes.Group;

/// <summary>
/// Bot被禁言
/// </summary>
public record MutedEvent : EventBase
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public override Events Type { get; set; } = Events.Muted;

    /// <summary>
    ///     被禁言的时间，单位：秒
    /// </summary>
    [JsonProperty("durationSeconds")]
    public string Period { get; set; }

    /// <summary>
    ///     禁言bot的操作者
    /// </summary>
    [JsonProperty("operator")]
    public Member Operator { get; set; }
}