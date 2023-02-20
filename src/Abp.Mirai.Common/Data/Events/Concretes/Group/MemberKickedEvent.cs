﻿using Abp.Mirai.Common.Data.Shared;
using Newtonsoft.Json;

namespace Abp.Mirai.Common.Data.Events.Concretes.Group;

/// <summary>
/// 某群员被踢出群
/// </summary>
public record MemberKickedEvent : EventBase
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public override Events Type { get; set; } = Events.MemberKicked;

    /// <summary>
    /// 当事人
    /// </summary>
    [JsonProperty("member")] public Member Member { get; set; }

    /// <summary>
    /// 踢人者
    /// </summary>
    [JsonProperty("operator")] public Member Operator { get; set; }
}