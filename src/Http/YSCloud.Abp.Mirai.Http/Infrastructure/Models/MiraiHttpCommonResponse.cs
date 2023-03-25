using Newtonsoft.Json.Linq;

namespace YSCloud.Abp.Mirai.Http.Infrastructure.Models
{
    public class MiraiHttpCommonResponse : IMiraiHttpResponse<JObject>
    {
        public string ErrorMessage { get; set; }

        public int ErrorCode { get; set; }

        public JObject Data { get; set; }
    }
}