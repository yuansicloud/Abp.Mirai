﻿using Newtonsoft.Json;

namespace YSCloud.Abp.Mirai.Common.Data.Messages.Concretes;

/// <summary>
/// xml消息
/// </summary>
public record XmlMessage : MessageBase
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public override Messages Type { get; set; } = Messages.Xml;

    /// <summary>
    /// xml文本
    /// </summary>
    [JsonProperty("xml")] public string Xml { get; set; }
}