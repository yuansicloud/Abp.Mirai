using Abp.Mirai.Http.Infrastructure.Models;
using Abp.Mirai.Http.Infrastructure.OptionsResolve;
using Abp.Mirai.Http.Utils.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace Abp.Mirai.Http.Infrastructure.Sessions
{
    public class DefaultMiraiHttpSessionProvider : IMiraiHttpSessionProvider, ISingletonDependency
    {
        private readonly IDistributedCache<MiraiHttpSession> _distributedCache;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMiraiHttpOptionsResolver _miraiHttpOptionsResolver;

        public DefaultMiraiHttpSessionProvider(IDistributedCache<MiraiHttpSession> distributedCache, IHttpClientFactory httpClientFactory, IMiraiHttpOptionsResolver miraiHttpOptionsResolver)
        {
            _distributedCache = distributedCache;
            _httpClientFactory = httpClientFactory;
            _miraiHttpOptionsResolver = miraiHttpOptionsResolver;
        }

        public virtual async Task<MiraiHttpSession> GetMiraiHttpSessionAsync(string qq)
        {
            var options = await _miraiHttpOptionsResolver.ResolveAsync();

            return await _distributedCache.GetOrAddAsync($"CurrentMiraiHttpSession:{qq}",
                async () =>
                {

                    var client = _httpClientFactory.CreateClient();

                    var baseUrl = $"{options.Host}:{options.Port}".EnsureEndsWith('/');

                    var verifyRequest = new VerifyRequest(options.VerifyKey);

                    var sessionStr = await (await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, baseUrl + HttpEndpoints.Verify.GetDescription())
                    {
                        Content = new StringContent(verifyRequest.ToString())
                    })).Content.ReadAsStringAsync();

                    var sessionJson = JObject.Parse(sessionStr);

                    var sessionObj = sessionJson.SelectToken("$.session");

                    if (sessionObj == null)
                    {
                        throw new NullReferenceException($"无法获取到 Session，Mirai API 返回的内容为：{sessionStr}");
                    }

                    var bindRequest = new BindRequest(sessionObj.ToString(), qq);

                    var bindStr = await (await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, baseUrl + HttpEndpoints.Bind.GetDescription())
                    {
                        Content = new StringContent(bindRequest.ToString())
                    })).Content.ReadAsStringAsync();

                    var bindJson = JObject.Parse(bindStr);

                    var bindObj = sessionJson.SelectToken("$.code");

                    if (bindObj?.ToString() != "0")
                    {
                        throw new NullReferenceException($"绑定session失败，Mirai API 返回的内容为：{bindStr}");
                    }

                    return new MiraiHttpSession(qq, sessionObj.ToString(), options.Host, options.Port);
                },
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
        }
    }
}
