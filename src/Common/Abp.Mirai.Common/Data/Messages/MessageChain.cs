using Abp.Mirai.Common.Data.Messages.Concretes;

namespace Abp.Mirai.Common.Data.Messages;

//just a definition in Data namespace
public partial class MessageChain : List<MessageBase>
{
    /// <summary>
    /// AddRange
    /// </summary>
    /// <param name="collection"></param>
    public MessageChain(IEnumerable<MessageBase> collection) : base(collection)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public MessageChain() : base()
    {
    }

    /// <summary>
    /// 获取消息链中的纯文本消息
    /// </summary>
    /// <returns>如果没有文本消息返回空字符串</returns>
    public string GetPlainMessage()
    {
        var plain = this.OfType<PlainMessage>().ToList();
        if (!plain.Any())
            return string.Empty;

        return plain.Select(x => x.Text).JoinAsString("");
    }

    /// <summary>
    /// 获取消息链中的文本消息并且保持原有连贯性
    /// </summary>
    /// <returns>如果没有文本消息返回空列表</returns>
    public List<string> GetSeparatedPlainMessage()
    {
        var plain = this.OfType<PlainMessage>().ToList();
        if (!plain.Any())
            return new List<string>();

        return plain.Select(x => x.Text).ToList();
    }

}