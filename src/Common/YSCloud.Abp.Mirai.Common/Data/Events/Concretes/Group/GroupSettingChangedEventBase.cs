﻿using Newtonsoft.Json;
using YSCloud.Abp.Mirai.Common.Data.Events;
using YSCloud.Abp.Mirai.Common.Data.Shared;

namespace YSCloud.Abp.Mirai.Common.Data.Events.Concretes.Group;

/// <summary>
/// 群设置改变基类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract record GroupSettingChangedEventBase<T> : EventBase
{
    /// <summary>
    ///     原来的
    /// </summary>
    [JsonProperty("origin")]
    public T Origin { get; set; }

    /// <summary>
    ///     目前的
    /// </summary>
    [JsonProperty("current")]
    public T Current { get; set; }

    /// <summary>
    ///     产生此事件的群
    /// </summary>
    [JsonProperty("group")]
    public Shared.Group Group { get; set; }

    /// <summary>
    ///     操作者
    /// </summary>
    [JsonProperty("operator")]
    public Member Operator { get; set; }
}