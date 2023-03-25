using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace YSCloud.Abp.Mirai.Common.Tests
{
    [DependsOn(typeof(AbpTestBaseModule),
        typeof(AbpAutofacModule))]
    public class AbpMiraiCommonTestsModule : AbpModule
    {

    }
}