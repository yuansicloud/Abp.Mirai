using Newtonsoft.Json;

namespace Abp.Mirai.Http.Infrastructure.Models
{
    /// <summary>
    /// 使用此请求校验并激活你的Session，同时将Session与一个已登录的Bot绑定
    /// </summary>
    public class BindRequest : MiraiHttpCommonRequest
    {
        /// <summary>
        /// 你的session key
        /// </summary>
        [JsonProperty("sessionKey")]
        public string? SessionKey { get; protected set; }

        /// <summary>
        /// Session将要绑定的Bot的qq号
        /// </summary>
        [JsonProperty("qq")]
        public string? QQ { get; protected set; }

        /// <summary>
        /// BindRequest
        /// </summary>
        /// <param name="sessionKey">你的session key</param>
        /// <param name="qq">Session将要绑定的Bot的qq号</param>
        public BindRequest(string sessionKey, string qq)
        {
            SessionKey = sessionKey;
            QQ = qq;
        }

        protected BindRequest()
        {

        }

    }
}
