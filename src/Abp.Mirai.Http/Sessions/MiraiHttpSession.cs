using Abp.Mirai.Http.Data.Sessions;

namespace Abp.Mirai.Http.Sessions
{
    public class MiraiHttpSession
    {
        public string QQ { get; set; }

        public string SessionKey { get; set; }

        /// <summary>
        ///     mirai-api-http本地服务器地址，比如：localhost:114514，或者构造一个ConnectConfig对象
        ///     <exception cref="InvalidAddressException">传入错误的地址将会抛出异常</exception>
        /// </summary>
        public HttpAdapterConfig Address { get; set; }
    }
}
