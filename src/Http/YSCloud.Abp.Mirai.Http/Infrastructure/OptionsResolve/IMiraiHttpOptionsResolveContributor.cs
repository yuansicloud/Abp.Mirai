namespace YSCloud.Abp.Mirai.Http.Infrastructure.OptionsResolve
{
    public interface IMiraiHttpOptionsResolveContributor
    {
        string Name { get; }

        [Obsolete("Please use asynchronous method.")]
        void Resolve(MiraiHttpResolveContext context);

        ValueTask ResolveAsync(MiraiHttpResolveContext context);
    }
}