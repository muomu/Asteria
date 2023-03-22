using Microsoft.AspNetCore.Http;

namespace Asteria.MutliTenancy
{
    class CompositeTenantIdFactory : ITenantIdFactory
    {
        public CompositeTenantIdFactory(IReadOnlyCollection<ITenantIdProvider> providers)
        {
            Providers = providers;
        }

        IReadOnlyCollection<ITenantIdProvider> Providers { get; }

        public async ValueTask<string?> GetTenantIdAsync(HttpContext context)
        {
            foreach (var x in Providers)
            {
                if (await x.GetTenantIdAsync(context).ConfigureAwait(false) is not null and var id)
                {
                    return id;
                }
            }

            return null;
        }
    }


}
