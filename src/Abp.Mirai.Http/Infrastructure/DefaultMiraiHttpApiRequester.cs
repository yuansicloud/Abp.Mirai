using Abp.Mirai.Http.Infrastructure;
using Abp.Mirai.Http.Infrastructure.Models;
using Abp.Mirai.Http.Infrastructure.Sessions;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.miraiHttp.Infrastructure
{
    [Dependency(TryRegister = true)]
    public class DefaultMiraiHttpApiRequester : IMiraiHttpApiRequester, ITransientDependency
    {
        private readonly IHttpClientFactory _httpClientFactory; 
        private readonly IMiraiHttpSessionProvider _httpSessionProvider;

        public DefaultMiraiHttpApiRequester(IHttpClientFactory httpClientFactory,
            IMiraiHttpSessionProvider SessionAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpSessionProvider = SessionAccessor;
        }

        public virtual async Task<TResponse> RequestAsync<TResponse>(string targetUrl, HttpMethod method, IMiraiHttpRequest? miraiHttpRequest = null, string? qq = null)
        {
            var responseMessage =
                await RequestGetHttpResponseMessageAsync(targetUrl, method, miraiHttpRequest, qq);
            
            var resultStr = await responseMessage.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<TResponse>(resultStr);
        }

        private async Task<HttpResponseMessage> RequestGetHttpResponseMessageAsync(string targetUrl, HttpMethod method,
            IMiraiHttpRequest? miraiHttpRequest = null, string? qq = null)
        {
            var client = _httpClientFactory.CreateClient();

            //targetUrl = targetUrl.EnsureEndsWith('?');

            var requestMsg = method == HttpMethod.Get
                ? BuildHttpGetRequestMessage(targetUrl, miraiHttpRequest)
                : BuildHttpPostRequestMessage(targetUrl, miraiHttpRequest);

            if (!qq.IsNullOrEmpty())
            {
                var session = await _httpSessionProvider.GetMiraiHttpSessionAsync(qq);

                requestMsg.Headers.Add("sessionKey", session.SessionKey);
            }

            return await client.SendAsync(requestMsg);
        }

        private HttpRequestMessage BuildHttpGetRequestMessage(string targetUrl, IMiraiHttpRequest miraiHttpRequest)
        {
            if (miraiHttpRequest == null) return new HttpRequestMessage(HttpMethod.Get, targetUrl);

            var requestUrl = BuildQueryString(targetUrl, miraiHttpRequest);
            return new HttpRequestMessage(HttpMethod.Get, requestUrl);
        }

        private HttpRequestMessage BuildHttpPostRequestMessage(string targetUrl, IMiraiHttpRequest miraiHttpRequest)
        {
            return new HttpRequestMessage(HttpMethod.Post, targetUrl)
            {
                Content = new StringContent(miraiHttpRequest.ToString())
            };
        }

        private string BuildQueryString(string targetUrl, IMiraiHttpRequest request)
        {
            if (request == null) return targetUrl;

            var type = request.GetType();
            var properties = type.GetProperties();

            if (properties.Length > 0)
            {
                targetUrl = targetUrl.EnsureEndsWith('&');
            }
            
            var queryStringBuilder = new StringBuilder(targetUrl);

            foreach (var propertyInfo in properties)
            {
                var jsonProperty = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
                var propertyName = jsonProperty != null ? jsonProperty.PropertyName : propertyInfo.Name;

                queryStringBuilder.Append($"{propertyName}={propertyInfo.GetValue(request)}&");
            }

            return queryStringBuilder.ToString().TrimEnd('&');
        }
    }
}