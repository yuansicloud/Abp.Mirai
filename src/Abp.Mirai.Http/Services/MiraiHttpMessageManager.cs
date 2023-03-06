using Abp.Mirai.Common.Data.Messages;
using Abp.Mirai.Common.Services;
using Abp.Mirai.Http.Infrastructure.Sessions;
using Abp.Mirai.Http.Utils.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.Mirai.Http.Services
{
    public class MiraiHttpMessageManager : MiraiHttpCommonService, IMessageManager
    {
        public Task<T> GetMessageReceiverByIdAsync<T>(string messageId, string target, string? qq = null) where T : MessageReceiverBase
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MessageReceiverBase>> GetRoamingMessagesAsync(string target, long timeStart = 0, long timeEnd = 0, string? qq = null)
        {
            throw new NotImplementedException();
        }

        public Task<string> QuoteFriendMessageAsync(string target, string messageId, MessageChain chain, string? qq = null)
        {
            throw new NotImplementedException();
        }

        public Task<string> QuoteGroupMessageAsync(string target, string messageId, MessageChain chain, string? qq = null)
        {
            throw new NotImplementedException();
        }

        public Task<string> QuoteTempMessageAsync(string memberId, string group, string messageId, MessageChain chain)
        {
            throw new NotImplementedException();
        }

        public Task RecallAsync(string messageId, string target, string? qq = null)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SendFriendMessageAsync(string friendId, MessageChain chain, string? qq = null)
        {
            var payload = new
            {
                target = friendId,
                messageChain = chain
            };

            var jObj = await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.SendFriendMessage, HttpMethod.Post, payload, qq);

            var messageId = jObj.SelectToken("$.messageId");

            if (messageId == null)
            {
                // Customize Error
                throw new NullReferenceException($"无法获取到 messageId，Mirai API 返回的内容为：{jObj}");
            }

            return messageId.ToString();
        }

        public Task<string> SendGroupMessageAsync(string groupId, MessageChain chain, string? qq = null)
        {
            throw new NotImplementedException();
        }

        public Task SendNudgeAsync(string target, string subject, MessageReceivers kind, string? qq = null)
        {
            throw new NotImplementedException();
        }

        public Task<string> SendTempMessageAsync(string target, string group, MessageChain chain, string? qq = null)
        {
            throw new NotImplementedException();
        }
    }
}
