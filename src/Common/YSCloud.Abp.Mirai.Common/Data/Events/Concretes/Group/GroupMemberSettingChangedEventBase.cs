using Newtonsoft.Json;
using YSCloud.Abp.Mirai.Common.Data.Events;
using YSCloud.Abp.Mirai.Common.Data.Shared;

namespace YSCloud.Abp.Mirai.Common.Data.Events.Concretes.Group;

/// <summary>
/// 群成员设置改变基类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract record GroupMemberSettingChangedEventBase<T> : EventBase
{
    /// <summary>
    /// 原来的值
    /// </summary>
    [JsonProperty("origin")] public T Origin { get; set; }

    /// <summary>
    /// 当前的值
    /// </summary>
    [JsonProperty("current")] public T Current { get; set; }

    /// <summary>
    ///     产生此事件的群
    /// </summary>
    [JsonProperty("group")]
    public Shared.Group Group { get; set; }

    /// <summary>
    ///     操作者
    /// </summary>
    [JsonProperty("member")]
    public Member Member { get; set; }
}