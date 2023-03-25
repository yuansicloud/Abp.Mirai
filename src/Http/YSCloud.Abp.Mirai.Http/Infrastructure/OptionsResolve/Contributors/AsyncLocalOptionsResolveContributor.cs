using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using YSCloud.Abp.Mirai.Http.Infrastructure.OptionsResolve;

namespace YSCloud.Abp.Mirai.Http.Infrastructure.OptionsResolve.Contributors
{
    public class AsyncLocalOptionsResolveContributor : IMiraiHttpOptionsResolveContributor
    {
        public const string ContributorName = "AsyncLocal";

        public string Name => ContributorName;

        public void Resolve(MiraiHttpResolveContext context)
        {
            var asyncLocal = context.ServiceProvider.GetRequiredService<IMiraiHttpAsyncLocalAccessor>();

            if (asyncLocal.Current != null)
            {
                context.Options = asyncLocal.Current;
            }
        }

        public ValueTask ResolveAsync(MiraiHttpResolveContext context)
        {
            var asyncLocal = context.ServiceProvider.GetRequiredService<IMiraiHttpAsyncLocalAccessor>();

            if (asyncLocal.Current != null)
            {
                context.Options = asyncLocal.Current;
            }

            return new ValueTask();
        }
    }

    public interface IMiraiHttpAsyncLocalAccessor
    {
        IMiraiHttpOptions Current { get; set; }
    }

    public class MiraiHttpAsyncLocalAccessor : IMiraiHttpAsyncLocalAccessor, ISingletonDependency
    {
        public IMiraiHttpOptions Current
        {
            get => _asyncLocal.Value;
            set => _asyncLocal.Value = value;
        }

        private readonly AsyncLocal<IMiraiHttpOptions> _asyncLocal;

        public MiraiHttpAsyncLocalAccessor()
        {
            _asyncLocal = new AsyncLocal<IMiraiHttpOptions>();
        }
    }

    public interface IMiraiHttpAsyncLocal
    {
        IMiraiHttpOptions CurrentOptions { get; }

        IDisposable Change(IMiraiHttpOptions weChatOfficialOptions);
    }

    public class MiraiHttpAsyncLocal : IMiraiHttpAsyncLocal, ITransientDependency
    {
        public IMiraiHttpOptions CurrentOptions { get; private set; }

        private readonly IMiraiHttpAsyncLocalAccessor _miraiHttpAsyncLocalAccessor;

        public MiraiHttpAsyncLocal(IMiraiHttpAsyncLocalAccessor miraiHttpAsyncLocalAccessor)
        {
            _miraiHttpAsyncLocalAccessor = miraiHttpAsyncLocalAccessor;

            CurrentOptions = miraiHttpAsyncLocalAccessor.Current;
        }

        public IDisposable Change(IMiraiHttpOptions miraiHttpOptions)
        {
            var parentScope = _miraiHttpAsyncLocalAccessor.Current;

            _miraiHttpAsyncLocalAccessor.Current = miraiHttpOptions;

            return new DisposeAction(() =>
            {
                _miraiHttpAsyncLocalAccessor.Current = parentScope;
            });
        }
    }
}