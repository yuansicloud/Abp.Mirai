using Abp.Mirai.Http.Infrastructure.OptionsResolve.Contributors;
using JetBrains.Annotations;

namespace Abp.Mirai.Http.Infrastructure.OptionsResolve
{
    public class AbpMiraiHttpResolveOptions
    {
        [NotNull] public List<IMiraiHttpOptionsResolveContributor> MiraiHttpOptionsResolveContributors { get; }

        public AbpMiraiHttpResolveOptions()
        {
            MiraiHttpOptionsResolveContributors = new List<IMiraiHttpOptionsResolveContributor>
            {
                new ConfigurationOptionsResolveContributor()
            };
        }
    }
}