using Microsoft.AspNetCore.Http;

namespace Asteria.MutliTenancy
{
    /// <summary>
    /// 租户工厂
    /// </summary>
    public interface ITenantIdFactory
    {
        /// <summary>
        /// 获取租户id
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        ValueTask<string?> GetTenantIdAsync(HttpContext context);
    }
}
