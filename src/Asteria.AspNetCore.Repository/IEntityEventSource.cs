namespace Asteria.Repository
{
    /// <summary>
    /// 提供实体修改事件的统一调度
    /// </summary>
    public interface IEntityEventSource
    {
        /// <summary>
        /// 当实体即将保存修改时引发的事件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="eventType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask OnSaveBeforeAsync(object entity, EntityEventType eventType, CancellationToken cancellationToken = default);

        /// <summary>
        /// 当实体修改提交后引发的事件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="eventType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask OnSaveAfterAsync(object entity, EntityEventType eventType, CancellationToken cancellationToken = default);
    }
}
