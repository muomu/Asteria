using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace Asteria.Repository.EntityFrameworkCore
{
    /// <summary>
    /// EFCore工作单元
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class EntityFrameworkUnitOfWork<TDbContext> : IUnitOfWork, IInternalAccessor<DbContext> where TDbContext : DbContext
    {
        /// <summary>
        /// 实例化EFCore工作单元
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="eventSource"></param>
        /// <param name="queryFilter"></param>
        public EntityFrameworkUnitOfWork(TDbContext dbContext, IEntityEventSource eventSource, IQueryFilter queryFilter)
        {
            DbContext = dbContext;
            EventSource = eventSource;
            QueryFilter = queryFilter;
        }

        private bool __disposed = false;
        private readonly ConcurrentDictionary<Type, object> __repositoriesCache = new();

        DbContext IInternalAccessor<DbContext>.Value => DbContext;

        /// <summary>
        /// 获取当前工作单元的实体事件源
        /// </summary>
        protected IEntityEventSource EventSource { get; }

        /// <summary>
        /// 获取当前工作单元的查询过滤器
        /// </summary>
        protected IQueryFilter QueryFilter { get; }

        /// <summary>
        /// 获取数据库上下文对象
        /// </summary>
        protected virtual DbContext DbContext { get; }



        /// <inheritdoc/>
        public virtual async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            Dictionary<object, EntityEventType> tracking = new();

            foreach (var x in DbContext.ChangeTracker.Entries())
            {
                if (x.State is EntityState.Unchanged or EntityState.Detached) continue;
                tracking.Add(x.Entity, x.State switch
                {
                    EntityState.Added => EntityEventType.Added,
                    EntityState.Modified => EntityEventType.Modified,
                    EntityState.Deleted => EntityEventType.Deleted,
                    _ => throw new InvalidOperationException()
                });
            }

            if (tracking.Any())
            {
                foreach (var x in tracking)
                {
                    await EventSource.OnSaveBeforeAsync(x.Key, x.Value, cancellationToken);
                }

                int lines = await DbContext.SaveChangesAsync(cancellationToken);

                foreach (var x in tracking)
                {
                    await EventSource.OnSaveAfterAsync(x.Key, x.Value, cancellationToken);
                }

                return lines;
            }
            else
            {
                return 0;
            }
        }

        /// <inheritdoc/>
        public virtual async Task ExecuteTransactionAsync(Action<IUnitOfWork> unitOfworkFunc, CancellationToken cancellationToken = default)
        {
            using var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                unitOfworkFunc(this);
                if (await CommitAsync(cancellationToken) > 0)
                {
                    await transaction.CommitAsync(cancellationToken);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        /// <inheritdoc/>
        public virtual async Task ExecuteTransactionAsync(Func<IUnitOfWork, Task> unitOfworkFunc, CancellationToken cancellationToken = default)
        {
            using var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await unitOfworkFunc(this).ConfigureAwait(false);
                if (await CommitAsync(cancellationToken) > 0)
                {
                    await transaction.CommitAsync(cancellationToken);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        /// <inheritdoc/>
        public virtual async Task<TResult> ExecuteTransactionAsync<TResult>(Func<IUnitOfWork, TResult> unitOfworkFunc, CancellationToken cancellationToken = default)
        {
            using var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                TResult result = unitOfworkFunc(this);
                if (await CommitAsync(cancellationToken) > 0)
                {
                    await transaction.CommitAsync(cancellationToken);
                }

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        /// <inheritdoc/>
        public virtual async Task<TResult> ExecuteTransactionAsync<TResult>(Func<IUnitOfWork, Task<TResult>> unitOfworkFunc, CancellationToken cancellationToken = default)
        {
            using var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                TResult result = await unitOfworkFunc(this).ConfigureAwait(false);
                if (await CommitAsync(cancellationToken) > 0)
                {
                    await transaction.CommitAsync(cancellationToken);
                }

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        /// <inheritdoc/>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class => (__repositoriesCache.GetOrAdd(typeof(TEntity), new EntityFrameworkRepository<TEntity>(this, QueryFilter)) as IRepository<TEntity>)!;

        /// <summary>
        /// 释放当前对象使用的所有资源
        /// </summary>
        /// <param name="disposing">是否在释放状态</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!__disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            __disposed = true;
        }

        ///<inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public virtual async ValueTask DisposeAsync()
        {
            if (!__disposed)
            {
                await DbContext.DisposeAsync();
                GC.SuppressFinalize(this);
            }
            __disposed = true;
        }
    }
}
