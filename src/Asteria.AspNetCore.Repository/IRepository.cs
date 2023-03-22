namespace Asteria.Repository
{

    /// <summary>
    /// 提供对实体的状态修改和查询而无需关注状态提供程序实现的数据仓储能力
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IQueryable<TEntity>, IAsyncEnumerable<TEntity> where TEntity : class
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }


        /// <summary>
        /// 获取指定主键的实体，如果一级缓存（内存）中查询命中指定键的实体，则不会向状态提供程序发送查询请求
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity?> GetAsync(object[] keys, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更改指定的实体，将其状态标记为插入
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);
        /// <summary>
        /// 更改一组指定的实体，将其状态标记为插入
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更改指定的实体，将其状态置为修改
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// 更改指定的实体，将其状态置为删除
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// 更改一组指定的实体，将其状态置为删除
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<TEntity> entities);
    }




}
