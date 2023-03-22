using Asteria.Repository;

namespace Asteria.MutliTenancy
{
    class MutliTenantModelQueryFilterProvider<TEntity> : IQueryFilterProvider<TEntity> where TEntity : class
    {
        ITenantIdAccessor TenantIdAccessor { get; }

        public MutliTenantModelQueryFilterProvider(ITenantIdAccessor tenantIdAccessor)
        {
            TenantIdAccessor = tenantIdAccessor;
        }

        public IQueryable<TEntity> QueryFilter(IQueryable<TEntity> queryable)
        {
            if (typeof(TEntity).IsAssignableTo(typeof(ITenantModel)))
            {
                return queryable.Where(e => (e as ITenantModel)!.TenantId == TenantIdAccessor.TenantId);
            }

            return queryable;
        }
    }
}
