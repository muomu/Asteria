using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asteria.AspNetCore.Extensions
{
    /// <summary>
    /// 分页结果
    /// </summary>
    public class PaginationResult<T> : JsonResult
    {
        Pagination<T> Pagination { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        public PaginationResult(Pagination<T> pagination) : base(pagination.Items)
        {
            Pagination = pagination;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="offset"></param>
        /// <param name="maximum"></param>
        public PaginationResult(ICollection<T> result, int offset, int maximum) : base(result)
        {
            Pagination = new Pagination<T>(result, offset, maximum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="offset"></param>
        /// <param name="maximum"></param>
        public PaginationResult(IEnumerable<T> result, int offset, int maximum) : base(result)
        {
            Pagination = new Pagination<T>(result.ToArray(), offset, maximum);
        }

        private void WriteHeader(HttpResponse response)
        {
            response.Headers["Content-Range"] = $"items {Pagination.Offset}-{Pagination.Highest}/{Pagination.Maximum}";
        }

        /// <inheritdoc/>
        public override void ExecuteResult(ActionContext context)
        {
            WriteHeader(context.HttpContext.Response);
            base.ExecuteResult(context);
        }

        /// <inheritdoc/>
        public override Task ExecuteResultAsync(ActionContext context)
        {
            WriteHeader(context.HttpContext.Response);
            return base.ExecuteResultAsync(context);
        }
    }
}
