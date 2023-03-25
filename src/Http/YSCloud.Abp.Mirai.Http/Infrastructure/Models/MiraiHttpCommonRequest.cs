using Newtonsoft.Json;

namespace YSCloud.Abp.Mirai.Http.Infrastructure.Models
{
    public abstract class MiraiHttpCommonRequest : IMiraiHttpRequest
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}