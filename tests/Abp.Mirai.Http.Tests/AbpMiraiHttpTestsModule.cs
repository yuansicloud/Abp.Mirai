using Abp.Mirai.Common.Tests;
using Volo.Abp.Modularity;

namespace Abp.Mirai.Http.Tests
{
    [DependsOn(typeof(AbpMiraiCommonTestsModule),
        typeof(AbpMiraiHttpModule))]
    public class AbpMiraiHttpTestsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMiraiHttpOptions>(op =>
            {
                op.Host = AbpMiraiHttpTestsConsts.Host;
                op.Port = AbpMiraiHttpTestsConsts.Port;
                op.VerifyKey = AbpMiraiHttpTestsConsts.VerifyKey;
                op.PollingRate = AbpMiraiHttpTestsConsts.PollingRate;
            });
        }
    }
}