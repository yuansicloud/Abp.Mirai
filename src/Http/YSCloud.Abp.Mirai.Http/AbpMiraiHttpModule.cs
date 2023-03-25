using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;
using YSCloud.Abp.Mirai.Common;
using YSCloud.Abp.Mirai.Http.Infrastructure.OptionsResolve;
using YSCloud.Abp.Mirai.Http.Infrastructure.OptionsResolve.Contributors;

namespace YSCloud.Abp.Mirai.Http
{
    [DependsOn(
        typeof(AbpMiraiCommonModule),
        typeof(AbpCachingModule))]
    public class AbpMiraiHttpModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClient();
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMiraiHttpResolveOptions>(options =>
            {
                if (!options.MiraiHttpOptionsResolveContributors.Exists(x => x.Name == AsyncLocalOptionsResolveContributor.ContributorName))
                {
                    options.MiraiHttpOptionsResolveContributors.Insert(0, new AsyncLocalOptionsResolveContributor());
                }
            });
        }
    }
}