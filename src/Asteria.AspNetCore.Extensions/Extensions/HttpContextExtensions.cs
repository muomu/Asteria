using Asteria;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Http
{


    /// <summary>
    /// HttpContext 方法扩展
    /// </summary>
    public static class HttpContextExtensions
    {

        /// <summary>
        /// 获取当前登录的用户身份ID
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static UserIdentifier GetCurrentUserNameIdentifier(this HttpContext httpContext)
        {
            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            if (httpContext.User is null)
            {
                throw new InvalidOperationException("当前上下文用户未登录");
            }
            if (httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value is not string identifier)
            {
                throw new InvalidOperationException("当前上下文用户未声明 NameIdentifier.");
            }

            return new UserIdentifier(identifier);
        }

    }
}
