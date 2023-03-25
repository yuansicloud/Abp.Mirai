using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using YSCloud.Abp.Mirai.Common;

namespace YSCloud.Abp.Mirai.Webhook
{
    [DependsOn(
        typeof(AbpMiraiCommonModule))]
    public class AbpMiraiWebhookModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //context.Services.ConfigureOptions<AbpMiraiWebhookOptions>();
        }

    }
}