using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Testing;

namespace YSCloud.Abp.Mirai.Common.Tests
{
    public class AbpMiraiCommonTestBase<TStartupModule> : AbpIntegratedTest<TStartupModule> where TStartupModule : IAbpModule
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }
    }
}