using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Asteria.Repository
{
    /// <summary>
    /// 仓储方法扩展
    /// </summary>
    public static class IRepositoryExtensions
    {
        /// <summary>
        /// 提交该仓储的工作单元的所有修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="repository"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task CommitAsync<TEntity>(this IRepository<TEntity> repository, CancellationToken cancellationToken = default) where TEntity : class
        {
            await repository.UnitOfWork.CommitAsync(cancellationToken);
        }

        /// <summary>
        /// 使用表达式删除指定的实体并立即提交修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="repository"></param>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<int> RemoveAllAsync<TEntity>(this IRepository<TEntity> repository, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) where TEntity : class
        {
            var entitres = await repository.Where(predicate).ToArrayAsync(cancellationToken);
            repository.RemoveRange(entitres);
            await repository.CommitAsync(cancellationToken);
            return entitres.Length;
        }


    }
}
