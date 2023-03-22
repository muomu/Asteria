using System.Diagnostics.CodeAnalysis;

namespace Asteria.Repository.Extensions
{
    /// <summary>
    /// 仓储方法扩展
    /// </summary>
    public static class IRepositoryExtensions
    {
        /// <summary>
        /// 获取指定主键的实体
        /// </summary>
        /// <typeparam name="TKey">将要查询的主键类型</typeparam>
        /// <typeparam name="TEntity">将要查询的仓储的实体类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="key">将要查询的主键的值</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<TEntity?> GetAsync<TKey, TEntity>(this IRepository<TEntity> repository, [DisallowNull] TKey key, CancellationToken cancellationToken = default) where TEntity : class
        {
            return repository.GetAsync(new object[] { key }, cancellationToken);
        }

        /// <summary>
        /// 获取指定复合主键的实体
        /// </summary>
        /// <typeparam name="TKey1">主键1的类型</typeparam>
        /// <typeparam name="TKey2">主键2的类型</typeparam>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="repository"></param>
        /// <param name="key1">复合主键的第一个键的值</param>
        /// <param name="key2">复合主键的第二个键的值</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<TEntity?> GetAsync<TKey1, TKey2, TEntity>(this IRepository<TEntity> repository, [DisallowNull] TKey1 key1, [DisallowNull] TKey2 key2, CancellationToken cancellationToken = default) where TEntity : class
        {
            return repository.GetAsync(new object[] { key1, key2 }, cancellationToken);
        }

        /// <summary>
        /// 获取指定复合主键的实体
        /// </summary>
        /// <typeparam name="TKey1">主键1的类型</typeparam>
        /// <typeparam name="TKey2">主键2的类型</typeparam>
        /// <typeparam name="TKey3">主键3的类型</typeparam>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="repository"></param>
        /// <param name="key1">复合主键的第一个键的值</param>
        /// <param name="key2">复合主键的第二个键的值</param>
        /// <param name="key3">复合主键的第三个键的值</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<TEntity?> GetAsync<TKey1, TKey2, TKey3, TEntity>(this IRepository<TEntity> repository, [DisallowNull] TKey1 key1, [DisallowNull] TKey2 key2, [DisallowNull] TKey3 key3, CancellationToken cancellationToken = default) where TEntity : class
        {
            return repository.GetAsync(new object[] { key1, key2, key3 }, cancellationToken);
        }
    }
}
