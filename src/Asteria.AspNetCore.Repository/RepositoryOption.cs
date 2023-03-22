using Microsoft.Extensions.DependencyInjection;

namespace Asteria.Repository
{
    /// <summary>
    /// 定义仓储配置的入口
    /// </summary>
    public class RepositoryOption
    {
        /// <summary>
        /// 获取依赖注入容器构造对象
        /// </summary>
        public IServiceCollection Services { get; init; } = null!;

        /// <summary>
        /// 为指定实体增加IoC事件监听器
        /// </summary>
        /// <typeparam name="TEntity">要监听的实体</typeparam>
        /// <typeparam name="TEntityEventListener">事件监听器</typeparam>
        /// <returns></returns>
        public RepositoryOption AddEntityEventListener<TEntity, TEntityEventListener>() where TEntity : class where TEntityEventListener : EntityEventListener<TEntity>
        {
            if (!Services.Any(e => e.ServiceType == typeof(EntityEventListener<TEntity>) && e.ImplementationType == typeof(TEntityEventListener)))
            {
                Services.AddScoped<EntityEventListener<TEntity>, TEntityEventListener>();
            }

            return this;
        }

        /// <summary>
        /// 为指定实体增加IoC查询过滤器提供程序
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TQueryFilterProvider"></typeparam>
        /// <returns></returns>
        public RepositoryOption AddQueryFilterProvider<TEntity, TQueryFilterProvider>() where TEntity : class where TQueryFilterProvider : class, IQueryFilterProvider<TEntity>
        {
            if (!Services.Any(e => e.ServiceType == typeof(IQueryFilterProvider<TEntity>) && e.ImplementationType == typeof(TQueryFilterProvider)))
            {
                Services.AddScoped<IQueryFilterProvider<TEntity>, TQueryFilterProvider>();
            }

            return this;
        }
    }
}
