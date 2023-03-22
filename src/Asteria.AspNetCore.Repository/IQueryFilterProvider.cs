namespace Asteria.Repository
{
    /// <summary>
    /// 提供对查询过滤器的实现的扩展
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public interface IQueryFilterProvider<TEntity> where TEntity : class
    {
        /// <summary>
        /// 对指定实体的查询增加过滤表达式
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        IQueryable<TEntity> QueryFilter(IQueryable<TEntity> queryable);
    }
}
