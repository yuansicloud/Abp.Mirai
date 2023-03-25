using YSCloud.Abp.Mirai.Common.Data.Events;

namespace YSCloud.Abp.Mirai.Common.Handlers
{
    /// <summary>
    /// 定义了Mirai事件回调处理器。
    /// </summary>
    public interface IMiraiEventHandler
    {
        Task HandleAsync(EventBase context);
    }
}
