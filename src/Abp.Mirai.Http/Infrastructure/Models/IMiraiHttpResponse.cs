using Newtonsoft.Json;

namespace Abp.Mirai.Http.Infrastructure.Models
{
    public interface IMiraiHttpResponse<T>
    {
        [JsonProperty("msg")] string ErrorMessage { get; set; }

        [JsonProperty("code")] int ErrorCode { get; set; }

        [JsonProperty("data")] T Data { get; set; }
    }
}