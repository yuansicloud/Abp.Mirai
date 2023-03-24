using Abp.Mirai.Common.Data.Messages;

namespace Abp.Mirai.Common.Services
{
    public interface IMessageManager
    {

        /// <summary>
        /// 由消息id获取一条消息
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <param name="target">好友id或群id</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetMessageReceiverByIdAsync<T>(string messageId, string target, string? qq = null) where T : MessageReceiverBase;

        /// <summary>
        /// 获取漫游消息（目前仅支持好友）
        /// </summary>
        /// <param name="target">好友id或群id</param>
        /// <param name="timeStart">起始时间, UTC+8 时间戳, 单位为秒. 可以为 0, 即表示从可以获取的最早的消息起. 负数将会被看是 0.</param>
        /// <param name="timeEnd">结束时间, UTC+8 时间戳, 单位为秒. 可以为 <c>long.MaxValue</c>, 即表示到可以获取的最晚的消息为止. 低于 timeStart 的值将会被看作是 timeStart 的值.</param>
        /// <returns></returns>
        Task<IEnumerable<MessageReceiverBase>> GetRoamingMessagesAsync(string target, long timeStart = 0, long timeEnd = 0, string? qq = null);

        /// <summary>
        /// 发送好友消息
        /// </summary>
        /// <param name="target"></param>
        /// <param name="chain"></param>
        /// <returns></returns>
        Task<string> SendFriendMessageAsync(string friendId, MessageChain chain, string? qq = null);

        /// <summary>
        /// 发送群消息
        /// </summary>
        /// <param name="target"></param>
        /// <param name="chain"></param>
        /// <returns></returns>
        Task<string> SendGroupMessageAsync(string groupId, MessageChain chain, string? qq = null);

        /// <summary>
        /// 发送群临时消息
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="group"></param>
        /// <param name="chain"></param>
        /// <returns></returns>
        Task<string> SendTempMessageAsync(string target, string group, MessageChain chain, string? qq = null);

        /// <summary>
        /// 发送头像戳一戳
        /// </summary>
        /// <param name="target">戳一戳的目标</param>
        /// <param name="subject">在什么地方戳</param>
        /// <param name="kind">只可以选Friend, Strange和Group</param>
        Task SendNudgeAsync(string target, string subject, MessageReceivers kind, string? qq = null);

        /// <summary>
        ///  撤回消息
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <param name="target">好友id或群id</param>
        Task RecallAsync(string messageId, string target, string? qq = null);

        /// <summary>
        /// 回复好友消息
        /// </summary>
        /// <param name="target"></param>
        /// <param name="messageId"></param>
        /// <param name="chain"></param>
        /// <returns></returns>
        Task<string> QuoteFriendMessageAsync(string target, string messageId, MessageChain chain, string? qq = null);

        /// <summary>
        /// 回复群消息
        /// </summary>
        /// <param name="target"></param>
        /// <param name="messageId"></param>
        /// <param name="chain"></param>
        /// <returns></returns>
        Task<string> QuoteGroupMessageAsync(string target, string messageId, MessageChain chain, string? qq = null);

        /// <summary>
        /// 回复临时消息
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="group"></param>
        /// <param name="messageId"></param>
        /// <param name="chain"></param>
        /// <returns></returns>
        Task<string> QuoteTempMessageAsync(string memberId, string group, string messageId, MessageChain chain, string? qq = null);
    }
}
