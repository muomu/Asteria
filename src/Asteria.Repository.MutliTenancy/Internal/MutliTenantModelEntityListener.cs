using Asteria.Repository;

namespace Asteria.MutliTenancy
{
    class MutliTenantModelEntityListener<T> : EntityEventListener<T> where T : class
    {
        public MutliTenantModelEntityListener(ITenantIdAccessor tenantIdAccessor)
        {
            TenantIdAccessor = tenantIdAccessor;
        }

        ITenantIdAccessor TenantIdAccessor { get; }

        protected override ValueTask OnInsertBeforeAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (TenantIdAccessor.TenantId is null) throw new TenantException("多租户实体保存时发生错误，原因是提供的租户ID为空。");

            if (entity is ITenantModel m)
            {
                m.TenantId = TenantIdAccessor.TenantId;
            }

            return ValueTask.CompletedTask;
        }

        protected override ValueTask OnUpdateBeforeAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (TenantIdAccessor.TenantId is null) return ValueTask.CompletedTask;

            if (entity is ITenantModel m)
            {
                if (m.TenantId != TenantIdAccessor.TenantId)
                {
                    throw new TenantException($"多租户实体修改时发生错误，提供的租户ID({TenantIdAccessor.TenantId})与实体对象的租户ID（{m.TenantId}）不一致");
                }
            }

            return ValueTask.CompletedTask;
        }

        protected override ValueTask OnDeleteBeforeAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (TenantIdAccessor.TenantId is null) return ValueTask.CompletedTask;

            if (entity is ITenantModel m)
            {
                if (m.TenantId != TenantIdAccessor.TenantId)
                {
                    throw new TenantException($"多租户实体删除时发生错误，提供的租户ID({TenantIdAccessor.TenantId})与实体对象的租户ID（{m.TenantId}）不一致");
                }
            }

            return ValueTask.CompletedTask;
        }
    }
}
