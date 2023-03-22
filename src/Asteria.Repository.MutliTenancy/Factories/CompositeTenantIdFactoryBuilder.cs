using System.Collections.Concurrent;

namespace Asteria.MutliTenancy
{


    /// <summary>
    /// 租户id工厂构建器
    /// </summary>
    sealed class CompositeTenantIdFactoryBuilder : ITenantIdFactoryBuilder
    {
        public ConcurrentQueue<ITenantIdProvider> TenantIdProviders { get; } = new ConcurrentQueue<ITenantIdProvider>();

        public void AddProvider(ITenantIdProvider provider)
        {
            TenantIdProviders.Enqueue(provider);
        }

        public void ClearProvider()
        {
            TenantIdProviders.Clear();
        }

        public ITenantIdFactory Build(IServiceProvider serviceProvider)
        {
            var factory = new CompositeTenantIdFactory(TenantIdProviders);
            return factory;
        }


    }
}
