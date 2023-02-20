using Manganese.Text;
using Abp.Mirai.Common.Data.Exceptions;
using System.Linq;

namespace Abp.Mirai.Http.Data.Sessions
{

    /// <summary>
    /// 适配器配置
    /// </summary>
    /// <param name="Host"></param>
    /// <param name="Port"></param>
    public record HttpAdapterConfig(string Host, string Port)
    {
        /// <summary>
        /// 从string自动转换成ip:port对
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static implicit operator HttpAdapterConfig(string address)
        {
            address = address.TrimEnd('/').Empty("http://").Empty("https://");
            if (!address.Contains(':')) throw new InvalidAddressException($"错误的地址: {address}");

            var split = address.Split(':');

            if (split.Length != 2) throw new InvalidAddressException($"错误的地址: {address}");
            if (!split.Last().IsInteger()) throw new InvalidAddressException($"错误的地址: {address}");

            return new HttpAdapterConfig(split[0], split[1]);
        }

        /// <summary>
        /// 转换为string
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static implicit operator string(HttpAdapterConfig config)
        {
            return $"{config.Host}:{config.Port}";
        }

        /// <summary>
        /// this
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this;
        }
    }
}


namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}