using System.Linq.Expressions;
using System.Reflection;

namespace Asteria.AspNetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SortableQueryableExtensions
    {


        /// <summary>
        /// 使用SimpleSortInfo对象进行排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="sortColumn">将要进行排序的字段</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> ts, SortColumn sortColumn)
        {
            if (sortColumn == SortColumn.Empty)
            {
                return ts;
            }

            var propertyInfo = typeof(T).GetProperty(sortColumn.FieldName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.IgnoreCase);
            if (propertyInfo is null) throw new InvalidOperationException($"找不到属性 {sortColumn.FieldName}");

            var method = typeof(SortableQueryableExtensions).GetMethod(nameof(GetOrderedQueryable), BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.InvokeMethod)?.MakeGenericMethod(propertyInfo.ReflectedType!, propertyInfo.PropertyType);

            var pts = Expression.Parameter(typeof(IQueryable<T>));
            var pinfo = Expression.Parameter(typeof(SortColumn));

            var expression = Expression.Call(method!, pts, pinfo);

            var lambda = Expression.Lambda<Func<IQueryable<T>, SortColumn, IOrderedQueryable<T>>>(expression, pts, pinfo);
            var func = lambda.Compile();
            var result = func.Invoke(ts, sortColumn);

            return result;
        }

        private static IOrderedQueryable<T> GetOrderedQueryable<T, TKey>(IQueryable<T> ts, SortColumn column)
        {

            var expression = GetOrderExpression<T, TKey>(column);
            if (column.Inversed)
            {
                return ts.OrderByDescending(expression);
            }
            else
            {
                return ts.OrderBy(expression);
            }
        }

        private static Expression<Func<T, TKey>> GetOrderExpression<T, TKey>(SortColumn column)
        {
            var target = Expression.Parameter(typeof(T));
            var property = Expression.Property(target, column.FieldName);
            var lambda = Expression.Lambda<Func<T, TKey>>(property, target);

            return lambda;
        }
    }
}
