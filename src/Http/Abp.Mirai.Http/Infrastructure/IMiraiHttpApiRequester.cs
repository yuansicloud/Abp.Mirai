using System.Net.Http;
using System.Threading.Tasks;
using Abp.Mirai.Http.Infrastructure.Models;
using Abp.Mirai.Http.Infrastructure.Sessions;
using Newtonsoft.Json.Linq;
using Volo.Abp.DependencyInjection;

namespace Abp.Mirai.Http.Infrastructure
{
    public interface IMiraiHttpApiRequester
    {
        Task<JObject> RequestAsync(string targetUrl, HttpMethod method, object? miraiHttpRequest = null, string? qq = null);

        Task<JObject> RequestAsync(HttpEndpoints targetEndPoint, HttpMethod method, object? miraiHttpRequest = null, string? qq = null);
    }
}