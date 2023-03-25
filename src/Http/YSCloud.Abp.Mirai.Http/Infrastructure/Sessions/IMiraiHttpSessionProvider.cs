namespace YSCloud.Abp.Mirai.Http.Infrastructure.Sessions
{
    public interface IMiraiHttpSessionProvider
    {
        Task<MiraiHttpSession> GetMiraiHttpSessionAsync(string qq);
    }
}
