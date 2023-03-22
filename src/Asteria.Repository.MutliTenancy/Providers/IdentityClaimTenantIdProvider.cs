using Microsoft.AspNetCore.Http;

namespace Asteria.MutliTenancy
{
    class IdentityClaimTenantIdProvider : ITenantIdProvider
    {
        public IdentityClaimTenantIdProvider(string claimType = "tenantid")
        {
            ClaimType = claimType;
        }

        string ClaimType { get; }

        public ValueTask<string?> GetTenantIdAsync(HttpContext context)
        {
            if (context?.User?.FindFirst(ClaimType)?.Value is not null and var val)
            {
                return new ValueTask<string?>(val);
            }
            else
            {
                return new ValueTask<string?>((string?)null);
            }
        }
    }
}
