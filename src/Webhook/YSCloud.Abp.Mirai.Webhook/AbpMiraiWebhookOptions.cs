namespace YSCloud.Abp.Mirai.Webhook
{
    public class AbpMiraiWebhookOptions
    {
        //用于和ChatBot进行验证的key, 为空则无需验证
        public string? AuthHeaderKey { get; set; }

        //用于和ChatBot进行验证的请求头名称
        public string AuthHeaderName { get; set; } = "AbpMiraiAuthHeader";
    }
}