﻿using Newtonsoft.Json;
using YSCloud.Abp.Mirai.Common.Data.Events;

namespace YSCloud.Abp.Mirai.Common.Data.Events.Concretes.Group;

/// <summary>
/// Bot被某群踢了
/// </summary>
public record KickedEvent : EventBase
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public override Events Type { get; set; } = Events.Kicked;

    /// <summary>
    ///     bot被踢的群信息
    /// </summary>
    [JsonProperty("group")]
    public Shared.Group Group { get; set; }
}