using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace YSCloud.Abp.Mirai.Common.Data.Events;

/// <summary>
/// 事件基类
/// </summary>
public record EventBase
{
    /// <summary>
    /// 你又看不到这个
    /// </summary>
    protected EventBase()
    {
    }

    /// <summary>
    /// 事件类型
    /// </summary>
    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public virtual Events Type { get; set; }

    /// <summary>
    /// 接收事件的QQ号
    /// </summary>
    [JsonIgnore]
    public virtual string QQ { get; set; }

    /// <summary>
    /// ToString实际上是ToJsonString也就是说会被序列化成JSON文本
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }
}