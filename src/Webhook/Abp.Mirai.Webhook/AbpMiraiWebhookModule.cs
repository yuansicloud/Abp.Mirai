﻿using Abp.Mirai.Common;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Abp.Mirai.Webhook
{
    [DependsOn(
        typeof(AbpMiraiCommonModule))]
    public class AbpMiraiWebhookModule : AbpModule 
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.ConfigureOptions<AbpMiraiWebhookOptions>();
        }

    }
}