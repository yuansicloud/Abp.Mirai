using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Abp.Mirai.Http.Infrastructure.OptionsResolve
{
    public interface IMiraiHttpOptionsResolver
    {
        /// <summary>
        /// 解析微信公众号相关配置。
        /// </summary>
        [Obsolete("Please use asynchronous method.")]
        IMiraiHttpOptions Resolve();

        /// <summary>
        /// 解析微信公众号相关配置。
        /// </summary>
        ValueTask<IMiraiHttpOptions> ResolveAsync();
    }
}