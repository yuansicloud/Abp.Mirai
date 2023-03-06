using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Abp.Mirai.Http.Infrastructure.OptionsResolve
{
    public class MiraiHttpOptionsResolver : IMiraiHttpOptionsResolver, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AbpMiraiHttpResolveOptions _options;

        public MiraiHttpOptionsResolver(IServiceProvider serviceProvider,
            IOptions<AbpMiraiHttpResolveOptions> abpMiraiHttpResolveOptions)
        {
            _serviceProvider = serviceProvider;
            _options = abpMiraiHttpResolveOptions.Value;
        }

        [Obsolete("Please use asynchronous method.")]
        public IMiraiHttpOptions Resolve()
        {
            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var context = new MiraiHttpResolveContext(serviceScope.ServiceProvider);

                foreach (var resolver in _options.MiraiHttpOptionsResolveContributors)
                {
                    resolver.Resolve(context);

                    if (context.Options != null)
                    {
                        return context.Options;
                    }
                }
            }

            return new AbpMiraiHttpOptions();
        }

        public virtual async ValueTask<IMiraiHttpOptions> ResolveAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = new MiraiHttpResolveContext(scope.ServiceProvider);

                foreach (var contributor in _options.MiraiHttpOptionsResolveContributors)
                {
                    await contributor.ResolveAsync(context);

                    if (context.Options != null)
                    {
                        return context.Options;
                    }
                }
            }

            return new AbpMiraiHttpOptions();
        }
    }
}