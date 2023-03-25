using Newtonsoft.Json;

namespace YSCloud.Abp.Mirai.Http.Infrastructure.Models
{
    /// <summary>
    /// 使用此请求验证你的身份，并返回一个会话
    /// </summary>
    public class VerifyRequest : MiraiHttpCommonRequest
    {
        /// <summary>
        /// 创建Mirai-Http-Server时生成的key，可在启动时指定或随机生成
        /// </summary>
        [JsonProperty("verifyKey")]
        public string? VerifyKey { get; protected set; }

        /// <summary>
        /// VerifyRequest
        /// </summary>
        /// <param name="verifyKey">创建Mirai-Http-Server时生成的key，可在启动时指定或随机生成</param>
        public VerifyRequest(string? verifyKey)
        {
            VerifyKey = verifyKey;
        }

        protected VerifyRequest()
        {

        }

    }
}
