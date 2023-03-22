using Microsoft.AspNetCore.Http;
using System;

namespace Asteria.AspNetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SortableHttpRequestExtensions
    {
        /// <summary>
        /// 获取排序信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static SortColumn GetSortColumn(this HttpRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Headers.TryGetValue("sort", out var val))
            {
                return val.ToString();
            }

            return SortColumn.Empty;
        }
    }
}
