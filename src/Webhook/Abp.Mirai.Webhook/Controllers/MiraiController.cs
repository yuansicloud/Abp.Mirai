using Abp.Mirai.Common;
using Abp.Mirai.Common.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
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

        public MiraiController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Mirai模块提供的通知接口.
        /// </summary>
        [HttpPost]
        [Route("notify")]
        public virtual async Task<ActionResult> Notify()
        {
            var msgHandlers = LazyServiceProvider.LazyGetService<IEnumerable<IMiraiMessageHandler>>();
            var eventHandlers = LazyServiceProvider.LazyGetService<IEnumerable<IMiraiEventHandler>>();

            Request.EnableBuffering();

            using (var streamReader = new StreamReader(_httpContextAccessor.HttpContext.Request.Body))
            {
                var result = await streamReader.ReadToEndAsync();


                Request.Body.Position = 0;
            }

            return Ok();
        }

    }
}
