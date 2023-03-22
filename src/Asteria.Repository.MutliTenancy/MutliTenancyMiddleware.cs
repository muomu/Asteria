using Microsoft.AspNetCore.Http;

namespace Asteria.MutliTenancy
{
    /// <summary>
    /// 租户生命周期请求管道中间件
    /// </summary>
    public class MutliTenancyMiddleware : IMiddleware
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        public MutliTenancyMiddleware(ITenantIdFactory factory)
        {
            Factory = factory;
        }

        /// <summary>
        /// 获取工厂对象
        /// </summary>
        protected virtual ITenantIdFactory Factory { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await HandleHttpContext(context);
            await next(context);

        }

        /// <summary>
        /// 处理HttpContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual async ValueTask HandleHttpContext(HttpContext context)
        {
            var tenantId = await Factory.GetTenantIdAsync(context);
            context.Items[nameof(ITenantIdAccessor.TenantId)] = tenantId;
        }
    }
}
