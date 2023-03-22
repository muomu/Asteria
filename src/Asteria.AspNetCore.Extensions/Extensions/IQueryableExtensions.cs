using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace System.Linq
{
    /// <summary>
    /// IQueryable方法扩展
    /// </summary>
    public static class IQueryableExtensions
    {

        /// <summary>
        /// 当且仅当condition为真时附加where表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereWhen<T>(this IQueryable<T> ts,  bool condition, Expression<Func<T, bool>> predicate)
        {
            if (condition)
            {
                return ts.Where(predicate);
            }
            else
            {
                return ts;
            }
        }

        /// <summary>
        /// 当且仅当指定的对象不为Null时，附加where表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="obj"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIfNotNull<T>(this IQueryable<T> ts, [AllowNull] T obj, Expression<Func<T, bool>> predicate)
        {
            if (obj is not null)
            {
                return ts.Where(predicate);
            }
            else
            {
                return ts;
            }
        }


    }
}
