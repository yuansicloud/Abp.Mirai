using Abp.Mirai.Http.Infrastructure.OptionsResolve;

namespace EasyAbp.Abp.WeChat.Official
{
    public class AbpMiraiHttpOptions : IMiraiHttpOptions
    {
        public string Host { get; set; }

        public string Port { get; set; }

        public string VerifyKey { get; set; }

        public int PollingRate { get; set; } = 1000;
    }
}