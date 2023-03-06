using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Abp.Mirai.Http.Infrastructure.OptionsResolve.Contributors
{
    public class ConfigurationOptionsResolveContributor : IMiraiHttpOptionsResolveContributor
    {
        public const string ContributorName = "Configuration";
        public string Name => ContributorName;

        public void Resolve(MiraiHttpResolveContext context)
        {
            context.Options = context.ServiceProvider.GetRequiredService<IOptions<AbpMiraiHttpOptions>>().Value;
        }

        public ValueTask ResolveAsync(MiraiHttpResolveContext context)
        {
            context.Options = context.ServiceProvider.GetRequiredService<IOptions<AbpMiraiHttpOptions>>().Value;

            return new ValueTask();
        }
    }
}