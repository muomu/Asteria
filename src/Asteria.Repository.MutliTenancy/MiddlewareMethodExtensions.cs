using Asteria.MutliTenancy;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public static class MiddlewareMethodExtensions
    {
        /// <summary>
        /// 使用多租户隔离
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMutliTenancy(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MutliTenancyMiddleware>();
        }
    }
}
