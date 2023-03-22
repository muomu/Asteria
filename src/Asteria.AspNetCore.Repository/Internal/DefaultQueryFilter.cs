using Microsoft.Extensions.DependencyInjection;

namespace Asteria.Repository
{
    class DefaultQueryFilter : IQueryFilter
    {
        public DefaultQueryFilter(IServiceProvider serviceProvider)
        {
            Services = serviceProvider;
        }

        IServiceProvider Services { get; }

        public IQueryable<TEntity> QueryFilter<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        {
            return QueryFilter(queryable, Array.Empty<Type>());
        }

        public IQueryable<TEntity> QueryFilter<TEntity>(IQueryable<TEntity> queryable, Type[] ignoredFilterType) where TEntity : class
        {
            var providers = Services.GetServices<IQueryFilterProvider<TEntity>>();

            foreach (var x in providers.SkipWhile(x => ignoredFilterType.Any(y => y.Equals(x.GetType()))))
            {
                queryable = x.QueryFilter(queryable);
            }

            return queryable;
        }
    }
}
