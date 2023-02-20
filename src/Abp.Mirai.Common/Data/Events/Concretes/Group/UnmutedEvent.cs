﻿using Abp.Mirai.Common.Data.Shared;
using Newtonsoft.Json;

namespace Abp.Mirai.Common.Data.Events.Concretes.Group;

/// <summary>
/// Bot被解除禁言
/// </summary>
public record UnmutedEvent : EventBase
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public override Events Type { get; set; } = Events.Unmuted;

    /// <summary>
    ///     取消禁言bot的操作者
    /// </summary>
    [JsonProperty("operator")]
    public Member Operator { get; set; }
}