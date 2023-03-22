using System;
using System.Linq;

namespace Asteria.Repository
{
    /// <summary>
    /// 提供对仓储实体的查询过滤表达式
    /// </summary>
    public interface IQueryFilter
    {
        /// <summary>
        /// 为指定实体的查询增加查询过滤表达式
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <param name="queryable"></param>
        /// <returns></returns>
        IQueryable<TEntity> QueryFilter<TEntity>(IQueryable<TEntity> queryable) where TEntity : class;

        /// <summary>
        /// 为指定实体的查询增加查询过滤表达式
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <param name="queryable"></param>
        /// <param name="ignoredFilterType">要忽略的查询过滤提供程序</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryFilter<TEntity>(IQueryable<TEntity> queryable, Type[] ignoredFilterType) where TEntity : class;
    }
}
