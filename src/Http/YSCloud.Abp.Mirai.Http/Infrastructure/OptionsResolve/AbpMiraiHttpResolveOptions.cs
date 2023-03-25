using JetBrains.Annotations;
using YSCloud.Abp.Mirai.Http.Infrastructure.OptionsResolve.Contributors;

namespace YSCloud.Abp.Mirai.Http.Infrastructure.OptionsResolve
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