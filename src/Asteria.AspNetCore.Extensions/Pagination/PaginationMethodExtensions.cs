using Microsoft.AspNetCore.Mvc;

namespace Asteria.AspNetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class PaginationMethodExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static IQueryable<T> Slice<T>(this IQueryable<T> ts, int offset, int limit)
        {
            return ts.Skip(offset).Take(limit);
        }

        /// <summary>
        /// 返回分页对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="range"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<Pagination<T>> ToPaginationAsync<T>(this IQueryable<T> ts, Range range, CancellationToken cancellationToken = default)
        {

            //int offset = range.Start.Value;
            //var max = ts.Count();
            //T[] result;
            //if (range.Equals(Range.All))
            //{
            //    result = ts.ToArray();
            //}
            //else
            //{
            //    result = ts.Slice(offset, range.GetLength()).ToArrayAsync(cancellationToken);
            //}

            //return new Pagination<T>(result, offset, max);
            throw new NotImplementedException();
        }

        /// <summary>
        /// 返回分页模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Pagination<T> ToPagination<T>(this IList<T> list, Range range)
        {
            var items = list.Skip(range.Start.Value).Take(range.GetLength()).ToArray();
            return new Pagination<T>(items, range.Start.Value, list.Count);
        }


        /// <summary>
        /// 返回分页结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <param name="result"></param>
        /// <param name="offset"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public static PaginationResult<T> Pagination<T>(this ControllerBase _, ICollection<T> result, int offset, int maximum)
        {
            Pagination<T> pagination = new(result, offset, maximum);
            return pagination;
        }
    }
}
