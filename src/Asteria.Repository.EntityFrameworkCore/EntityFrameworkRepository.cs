using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;

namespace Asteria.Repository.EntityFrameworkCore
{
    /// <summary>
    /// EFCore 仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// 实例化EFCore 仓储
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="queryFilter"></param>
        public EntityFrameworkRepository(IUnitOfWork unitOfWork, IQueryFilter queryFilter)
        {
            UnitOfWork = unitOfWork;
            QueryFilter = queryFilter;

            Entities = (UnitOfWork as IInternalAccessor<DbContext>)?.Value.Set<TEntity>() ?? throw new InvalidOperationException();

            Queryable = QueryFilter.QueryFilter(Entities);
        }

        /// <inheritdoc/>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// 获取一个值，该值指定当前仓储的查询是否进行实体跟踪，False为跟踪，否则不跟踪
        /// </summary>
        protected internal bool IsReadOnly { get; set; }

        /// <summary>
        /// 获取当前仓储的查询过滤器
        /// </summary>
        protected virtual IQueryFilter QueryFilter { get; }

        /// <summary>
        /// 获取当前仓储的DbSet对象
        /// </summary>
        protected virtual DbSet<TEntity> Entities { get; }

        /// <summary>
        /// 获取当前仓储中最终使用的查询对象
        /// </summary>
        protected IQueryable<TEntity> Queryable { get; set; }

        /// <inheritdoc/>
        public Type ElementType => Queryable.ElementType;

        /// <inheritdoc/>
        public Expression Expression => Queryable.Expression;

        /// <inheritdoc/>
        public IQueryProvider Provider => Queryable.Provider;

        /// <inheritdoc/>
        public IEnumerator<TEntity> GetEnumerator()
        {
            return Queryable.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return Queryable.GetEnumerator();
        }

        /// <inheritdoc/>
        public void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        /// <inheritdoc/>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        /// <inheritdoc/>
        public void Update(TEntity entity)
        {
            var entry = Entities.Update(entity);
            if (entry.State != EntityState.Modified) entry.State = EntityState.Modified;
        }

        /// <inheritdoc/>
        public void Remove(TEntity entity)
        {
            Entities.Remove(entity);
        }

        /// <inheritdoc/>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> RemoveAllAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            var entitres = await Entities.Where(expression).ToListAsync(cancellationToken);
            if (entitres.Any())
            {
                Entities.RemoveRange(entitres);
            }
            return entitres;
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> IgnoredQueryFilter()
        {
            Queryable = Entities;
            return Queryable;
        }

        /// <inheritdoc/>
        public IQueryable<TEntity> IgnoredQueryFilter(params Type[] filterTypes)
        {
            Queryable = QueryFilter.QueryFilter(Entities, filterTypes);
            return Queryable;
        }

        /// <inheritdoc/>
        public async Task<TEntity?> GetAsync(object[] keys, CancellationToken cancellationToken = default)
        {
            var e = await Entities.FindAsync(keyValues: keys, cancellationToken: cancellationToken);
            if (e != null)
            {
                var linq = (new[] { e }).AsQueryable();
                if (QueryFilter.QueryFilter(linq).Any())
                {
                    return e;
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return (Queryable as IAsyncEnumerable<TEntity>)!.GetAsyncEnumerator(cancellationToken);
        }

    }

}
