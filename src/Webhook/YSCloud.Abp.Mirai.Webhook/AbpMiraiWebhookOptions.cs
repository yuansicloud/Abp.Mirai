namespace YSCloud.Abp.Mirai.Webhook
{
    public class AbpMiraiWebhookOptions
    {
        //���ں�ChatBot������֤��key, Ϊ����������֤
        public string? AuthHeaderKey { get; set; }

        //���ں�ChatBot������֤������ͷ����
        public string AuthHeaderName { get; set; } = "AbpMiraiAuthHeader";
    }
}