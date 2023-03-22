namespace Asteria.Repository
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// 执行事务代码
        /// </summary>
        /// <param name="unitOfworkFunc"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ExecuteTransactionAsync(Action<IUnitOfWork> unitOfworkFunc, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行事务代码
        /// </summary>
        /// <param name="unitOfworkFunc"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ExecuteTransactionAsync(Func<IUnitOfWork, Task> unitOfworkFunc, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行事务代码
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="unitOfworkFunc"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TResult> ExecuteTransactionAsync<TResult>(Func<IUnitOfWork, TResult> unitOfworkFunc, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行事务代码
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="unitOfworkFunc"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TResult> ExecuteTransactionAsync<TResult>(Func<IUnitOfWork, Task<TResult>> unitOfworkFunc, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取指定实体的仓储实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// 提交实体修改
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);

    }

}
