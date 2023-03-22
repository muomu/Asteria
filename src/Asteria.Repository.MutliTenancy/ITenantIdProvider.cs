using Microsoft.AspNetCore.Http;

namespace Asteria.MutliTenancy
{
    /// <summary>
    /// 定义TenantId提供程序所需要的成员
    /// </summary>
    public interface ITenantIdProvider
    {
        /// <summary>
        /// 获取租户id
        /// </summary>
        /// <returns></returns>
        ValueTask<string?> GetTenantIdAsync(HttpContext context);
    }
}
