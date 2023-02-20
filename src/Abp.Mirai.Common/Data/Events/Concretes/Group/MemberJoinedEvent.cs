﻿using Abp.Mirai.Common.Data.Shared;
using Newtonsoft.Json;

namespace Abp.Mirai.Common.Data.Events.Concretes.Group;

/// <summary>
/// 新成员入群
/// </summary>
public record MemberJoinedEvent : EventBase
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public override Events Type { get; set; } = Events.MemberJoined;

    /// <summary>
    /// 当事人
    /// </summary>
    [JsonProperty("member")] public Member Member { get; set; }

    /// <summary>
    /// 肇事者
    /// </summary>
    [JsonProperty("invitor")] public Member Invitor { get; set; }
}