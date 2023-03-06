using Abp.Mirai.Http.Infrastructure;
using Abp.Mirai.Http.Infrastructure.Models;
using Abp.Mirai.Http.Infrastructure.OptionsResolve;
using Abp.Mirai.Http.Infrastructure.Sessions;
using Abp.Mirai.Http.Utils.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private readonly IMiraiHttpOptionsResolver _miraiHttpOptionsResolver;

        public DefaultMiraiHttpApiRequester(IHttpClientFactory httpClientFactory,
            IMiraiHttpSessionProvider SessionAccessor,
            IMiraiHttpOptionsResolver miraiHttpOptionsResolver)
        {
            _httpClientFactory = httpClientFactory;
            _httpSessionProvider = SessionAccessor;
            _miraiHttpOptionsResolver = miraiHttpOptionsResolver;
        }

        public virtual async Task<JObject> RequestAsync(HttpEndpoints endPoint, HttpMethod method, object? miraiHttpRequest = null, string? qq = null)
        {
            var options = await _miraiHttpOptionsResolver.ResolveAsync();

            var hostUrl = options.Host.EnsureEndsWith(':') + options.Port;

            return await RequestAsync(hostUrl.EnsureEndsWith('/') + endPoint.GetDescription(), method, miraiHttpRequest, qq);
        }

        public virtual async Task<JObject> RequestAsync(string targetUrl, HttpMethod method, object? miraiHttpRequest = null, string? qq = null)
        {
            var responseMessage =
                await RequestGetHttpResponseMessageAsync(targetUrl, method, miraiHttpRequest, qq);
            
            var resultStr = await responseMessage.Content.ReadAsStringAsync();
            // Error Processing... Fetch Code etc...

            return JObject.Parse(resultStr);
        }

        private async Task<HttpResponseMessage> RequestGetHttpResponseMessageAsync(string targetUrl, HttpMethod method,
            object? miraiHttpRequest = null, string? qq = null)
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

        private HttpRequestMessage BuildHttpGetRequestMessage(string targetUrl, object miraiHttpRequest)
        {
            if (miraiHttpRequest == null) return new HttpRequestMessage(HttpMethod.Get, targetUrl);

            var requestUrl = BuildQueryString(targetUrl, miraiHttpRequest);
            return new HttpRequestMessage(HttpMethod.Get, requestUrl);
        }

        private HttpRequestMessage BuildHttpPostRequestMessage(string targetUrl, object miraiHttpRequest)
        {
            var content = JsonConvert.SerializeObject(miraiHttpRequest, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return new HttpRequestMessage(HttpMethod.Post, targetUrl)
            {
                Content = new StringContent(content)
            };
        }

        private string BuildQueryString(string targetUrl, object request)
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