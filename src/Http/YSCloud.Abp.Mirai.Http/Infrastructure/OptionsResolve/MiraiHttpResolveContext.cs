using System;
using Volo.Abp.DependencyInjection;

namespace YSCloud.Abp.Mirai.Http.Infrastructure.OptionsResolve
{
    public class MiraiHttpResolveContext : IServiceProviderAccessor
    {
        public IMiraiHttpOptions Options { get; set; }

        public IServiceProvider ServiceProvider { get; }

        public MiraiHttpResolveContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}