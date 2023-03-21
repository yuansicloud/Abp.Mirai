using Abp.Mirai.Common;
using Abp.Mirai.Common.Data.Events;
using Abp.Mirai.Common.Data.Events.Concretes.Message;
using Abp.Mirai.Common.Data.Messages;
using Abp.Mirai.Common.Data.Messages.Concretes;
using Abp.Mirai.Common.Data.Messages.Receivers;
using Abp.Mirai.Common.Handlers;
using Abp.Mirai.Common.Utils;
using Manganese.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUglify.Helpers;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.Mirai.Webhook.Controllers
{
    [RemoteService(Name = AbpMiraiRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpMiraiRemoteServiceConsts.ModuleName)]
    [ControllerName("Mirai")]
    [Route("/Mirai")]
    public class MiraiController : AbpControllerBase
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<MiraiController> _logger;

        public MiraiController(IHttpContextAccessor httpContextAccessor, ILogger<MiraiController> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        /// <summary>
        /// Mirai模块提供的通知接口.
        /// </summary>
        [HttpPost]
        [Route("notify")]
        public virtual async Task<ActionResult> Notify()
        {

            string? qq = _httpContextAccessor.HttpContext?.Request.Headers.FirstOrDefault(x => x.Key == "qq").Value;

            var streamReader = new StreamReader(_httpContextAccessor.HttpContext.Request.Body);

            var result = await streamReader.ReadToEndAsync(); 

            await ProcessWebhookData(result, qq);



            return Ok();
        }

        private async Task ProcessWebhookData(string data, string? qq)
        {
            var dataType = data.Fetch("type");
            if (dataType == null || dataType.IsNullOrEmpty())
            {
                throw new UserFriendlyException("Webhook传回错误的响应");
            }

            if (dataType.Contains("Message"))
            {
                var receiver = ReflectionUtils.GetMessageReceiverBase(data);

                receiver.QQ = qq;

                var rawChain = data.Fetch("messageChain");
                if (rawChain == null || rawChain.IsNullOrEmpty())
                {
                    throw new UserFriendlyException("Webhook传回错误的响应");
                }

                receiver.MessageChain = rawChain.DeserializeMessageChain();

                if (receiver.MessageChain.OfType<AtMessage>().Any(x => x.Target == qq))
                {
                    await HandleEvent(new AtEvent
                    {
                        Receiver = (receiver as GroupMessageReceiver)!,
                        QQ = qq
                    });
                }

                await HandleMessage(receiver);
            }
            else if (dataType.Contains("Event"))
            {
                var @event = ReflectionUtils.GetEventBase(data);
                @event.QQ = qq;
                await HandleEvent(@event);
            }
            else
            {
                // LOG EXCEPTION
                // _unknownMessageReceived.OnNext(data);
            }
        }

        private async Task HandleMessage(MessageReceiverBase context)
        {
            var msgHandlers = LazyServiceProvider.LazyGetService<IEnumerable<IMiraiMessageHandler>>();

            foreach (var handler in msgHandlers)
            {
                await handler.HandleAsync(context);
            }
        }

        private async Task HandleEvent(EventBase context)
        {
            var eventHandlers = LazyServiceProvider.LazyGetService<IEnumerable<IMiraiEventHandler>>();

            foreach (var handler in eventHandlers)
            {
                await handler.HandleAsync(context);
            }
        }
    }
}
