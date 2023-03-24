using Abp.Mirai.Common.Data.Exceptions;
using Abp.Mirai.Common.Data.Messages;
using Abp.Mirai.Common.Services;
using Abp.Mirai.Common.Utils;
using Abp.Mirai.Http.Infrastructure.Sessions;
using Manganese.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Abp.Mirai.Http.Services
{
    public class MiraiHttpMessageManager : MiraiHttpCommonService, IMiraiMessageManager
    {
        public async Task<T> GetMessageReceiverByIdAsync<T>(string messageId, string target, string? qq = null) where T : MessageReceiverBase
        {
            var payload = new
            {
                messageId,
                target
            };

            var jObj = await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.RoamingMessages, HttpMethod.Get, payload, qq);

            var receiver = JsonConvert.DeserializeObject<T>(jObj.ToString())!;
            receiver.MessageChain = jObj.Fetch("data").Fetch("messageChain").DeserializeMessageChain(); 

            return receiver;
        }

        public async Task<IEnumerable<MessageReceiverBase>> GetRoamingMessagesAsync(string target, long timeStart = 0, long timeEnd = 0, string? qq = null)
        {
            var payload = new
            {
                timeStart,
                timeEnd,
                target = target.ToInt64(),
            };

            var jObj = await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.RoamingMessages, HttpMethod.Post, payload, qq);

            return JsonConvert.DeserializeObject<IEnumerable<MessageReceiverBase>>(jObj.Fetch("data"));
        }

        public async Task<string> QuoteFriendMessageAsync(string target, string messageId, MessageChain chain, string? qq = null)
        {
            var payload = new
            {
                target,
                quote = messageId,
                messageChain = chain
            };

            var jObj = await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.SendFriendMessage, HttpMethod.Post, payload, qq);

            return GetMessageId(jObj);
        }

        public async Task<string> QuoteGroupMessageAsync(string target, string messageId, MessageChain chain, string? qq = null)
        {
            var payload = new
            {
                target,
                quote = messageId,
                messageChain = chain
            };

            var jObj = await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.SendGroupMessage, HttpMethod.Post, payload, qq);

            return GetMessageId(jObj);
        }

        public async Task<string> QuoteTempMessageAsync(string memberId, string group, string messageId, MessageChain chain, string? qq = null)
        {
            var payload = new
            {
                qq = memberId,
                group,
                quote = messageId,
                messageChain = chain
            };

            var jObj = await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.SendFriendMessage, HttpMethod.Post, payload, qq);

            return GetMessageId(jObj);
        }

        public async Task RecallAsync(string messageId, string target, string? qq = null)
        {
            var payload = new
            {
                target,
                messageId
            };

            await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.Recall, HttpMethod.Post, payload, qq);
        }

        public async Task<string> SendFriendMessageAsync(string friendId, MessageChain chain, string? qq = null)
        {
            var payload = new
            {
                target = friendId,
                messageChain = chain
            };

            var jObj = await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.SendFriendMessage, HttpMethod.Post, payload, qq);

            return GetMessageId(jObj);
        }

        public async Task<string> SendGroupMessageAsync(string groupId, MessageChain chain, string? qq = null)
        {
            var payload = new
            {
                target = groupId,
                messageChain = chain
            };

            var jObj = await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.SendGroupMessage, HttpMethod.Post, payload, qq);

            return GetMessageId(jObj);
        }

        public async Task SendNudgeAsync(string target, string subject, MessageReceivers kind, string? qq = null)
        {
            var payload = new
            {
                target,
                subject,
                kind = kind.ToString()
            };

            await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.SendNudge, HttpMethod.Post, payload, qq);
        }

        public async Task<string> SendTempMessageAsync(string target, string group, MessageChain chain, string? qq = null)
        {
            var payload = new
            {
                qq = target,
                group,
                messageChain = chain
            };

            JObject jObj = await MiraiHttpApiRequester.RequestAsync(HttpEndpoints.SendTempMessage, HttpMethod.Post, payload, qq);

            return GetMessageId(jObj);
        }

        #region Helper Method
        private static string GetMessageId(JObject message)
        {
            var messageId = message.Fetch("messageId");

            if (messageId == null)
            {
                // Customize Error
                throw new InvalidResponseException($"无法获取到 messageId，Mirai API 返回的内容为：{message}");
            }

            return messageId;
        }
        #endregion
    }
}
