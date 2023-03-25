using Newtonsoft.Json.Linq;
using YSCloud.Abp.Mirai.Http.Infrastructure.Sessions;

namespace YSCloud.Abp.Mirai.Http.Infrastructure
{
    public interface IMiraiHttpApiRequester
    {
        Task<JObject> RequestAsync(string targetUrl, HttpMethod method, object? miraiHttpRequest = null, string? qq = null);

        Task<JObject> RequestAsync(HttpEndpoints targetEndPoint, HttpMethod method, object? miraiHttpRequest = null, string? qq = null);
    }
}