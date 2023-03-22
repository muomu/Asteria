namespace Asteria.Repository
{
    /// <summary>
    /// 实体事件监听器
    /// </summary>
    /// <typeparam name="TEntity">要监听的实体类型</typeparam>
    public abstract class EntityEventListener<TEntity> : IEntityEventListener where TEntity : class
    {
        /// <summary>
        /// 当实体插入之前引发的操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual ValueTask OnInsertBeforeAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return ValueTask.CompletedTask;
        }

        /// <summary>
        /// 当实体插入完成之后引发的操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual ValueTask OnInsertAfterAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return ValueTask.CompletedTask;
        }

        /// <summary>
        /// 当实体更新之前引发的操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual ValueTask OnUpdateBeforeAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return ValueTask.CompletedTask;
        }

        /// <summary>
        /// 当实体更新之后引发的操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual ValueTask OnUpdateAfterAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return ValueTask.CompletedTask;
        }

        /// <summary>
        /// 当实体删除之前引发的操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual ValueTask OnDeleteBeforeAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return ValueTask.CompletedTask;
        }

        /// <summary>
        /// 当实体删除之后引发的操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual ValueTask OnDeleteAfterAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return ValueTask.CompletedTask;
        }


        ValueTask IEntityEventListener.OnSaveAfterAsync(object entity, EntityEventType eventType, CancellationToken cancellationToken)
        {
            if (entity is not TEntity m) return ValueTask.CompletedTask;

            var task = eventType switch
            {
                EntityEventType.Added => OnInsertAfterAsync(m, cancellationToken),
                EntityEventType.Modified => OnUpdateAfterAsync(m, cancellationToken),
                EntityEventType.Deleted => OnDeleteAfterAsync(m, cancellationToken),
                _ => ValueTask.CompletedTask
            };

            return task;
        }

        ValueTask IEntityEventListener.OnSaveBeforeAsync(object entity, EntityEventType eventType, CancellationToken cancellationToken)
        {
            if (entity is not TEntity m) return ValueTask.CompletedTask;

            var task = eventType switch
            {
                EntityEventType.Added => OnInsertBeforeAsync(m, cancellationToken),
                EntityEventType.Modified => OnUpdateBeforeAsync(m, cancellationToken),
                EntityEventType.Deleted => OnDeleteBeforeAsync(m, cancellationToken),
                _ => ValueTask.CompletedTask
            };

            return task;
        }
    }
}
