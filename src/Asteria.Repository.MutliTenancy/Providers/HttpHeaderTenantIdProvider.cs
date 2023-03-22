using Microsoft.AspNetCore.Http;

namespace Asteria.MutliTenancy
{

    class HttpHeaderTenantIdProvider : ITenantIdProvider
    {
        public HttpHeaderTenantIdProvider(string headerName = "x-tenant-id")
        {
            HeaderName = headerName;
        }

        string HeaderName { get; }

        public ValueTask<string?> GetTenantIdAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(HeaderName, out var val))
            {
                return new ValueTask<string?>(val.ToString());
            }
            else
            {
                return new ValueTask<string?>((string?)null);
            }
        }
    }
}
