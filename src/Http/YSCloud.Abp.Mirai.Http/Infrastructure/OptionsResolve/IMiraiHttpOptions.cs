namespace YSCloud.Abp.Mirai.Http.Infrastructure.OptionsResolve
{
    public interface IMiraiHttpOptions
    {
        /// <summary>
        /// 地址
        /// </summary>
        string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        string Port { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        string VerifyKey { get; set; }

        /// <summary>
        /// 轮询间隔。 单位ms
        /// </summary>
        int PollingRate { get; set; }

        /// <summary>
        /// 是否开启单 session 模式, 若为 true，则自动创建 session 绑定 console 中登录的 bot
        /// </summary>
        //bool SingleMode { get; set; }

    }
}