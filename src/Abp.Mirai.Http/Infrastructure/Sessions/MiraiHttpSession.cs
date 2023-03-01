namespace Abp.Mirai.Http.Infrastructure.Sessions
{
    public class MiraiHttpSession
    {
        public string QQ { get; set; }

        public string SessionKey { get; set; }

        public string Host { get; set; }

        public string Port { get; set; }

        public MiraiHttpSession(string qq, string sessionKey, string host, string port)
        {
            QQ = qq;
            SessionKey = sessionKey;
            Host = host;
            Port = port;
        }
    }
}
