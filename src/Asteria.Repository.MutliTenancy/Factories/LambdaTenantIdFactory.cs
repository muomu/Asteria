using Microsoft.AspNetCore.Http;

namespace Asteria.MutliTenancy
{
    class LambdaTenantIdFactory : ITenantIdFactory
    {

        public Func<HttpContext, ValueTask<string?>> Func { get; }

        public LambdaTenantIdFactory(Func<HttpContext, ValueTask<string?>> func)
        {
            Func = func;
        }

        public ValueTask<string?> GetTenantIdAsync(HttpContext context)
        {
            return Func(context);
        }
    }
}
