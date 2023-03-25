using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using YSCloud.Abp.Mirai.Http.Infrastructure;

namespace YSCloud.Abp.Mirai.Http.Services
{
    public abstract class MiraiHttpCommonService : ITransientDependency
    {
        public IServiceProvider ServiceProvider { get; set; }

        protected readonly object ServiceLocker = new object();
        protected TService LazyLoadService<TService>(ref TService service)
        {
            if (service == null)
            {
                lock (ServiceLocker)
                {
                    if (service == null)
                    {
                        service = ServiceProvider.GetRequiredService<TService>();
                    }
                }
            }

            return service;
        }

        protected IMiraiHttpApiRequester MiraiHttpApiRequester => LazyLoadService(ref _miraiHttpApiRequester);
        private IMiraiHttpApiRequester _miraiHttpApiRequester;
    }
}
