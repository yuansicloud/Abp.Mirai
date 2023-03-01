using System.Net.Http;
using System.Threading.Tasks;
using Abp.Mirai.Http.Infrastructure.Models;
using Volo.Abp.DependencyInjection;

namespace Abp.Mirai.Http.Infrastructure
{
    public interface IMiraiHttpApiRequester
    {
        Task<TResponse> RequestAsync<TResponse>(string targetUrl, HttpMethod method, IMiraiHttpRequest? miraiHttpRequest = null, string? qq = null);
       
    }
}