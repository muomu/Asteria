using Microsoft.AspNetCore.Http;

namespace Asteria.MutliTenancy
{
    class TenantIdAccessor : ITenantIdAccessor
    {
        public TenantIdAccessor(IHttpContextAccessor httpContextAccessor)
        {
            HttpContext = httpContextAccessor.HttpContext;
        }

        HttpContext HttpContext { get; }

        public string? TenantId => HttpContext.Items[nameof(ITenantIdAccessor.TenantId)]?.ToString();
    }
}
