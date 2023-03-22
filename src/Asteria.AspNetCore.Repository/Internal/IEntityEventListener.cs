namespace Asteria.Repository
{
    interface IEntityEventListener
    {
        ValueTask OnSaveBeforeAsync(object entity, EntityEventType eventType, CancellationToken cancellationToken = default);
        ValueTask OnSaveAfterAsync(object entity, EntityEventType eventType, CancellationToken cancellationToken = default);
    }
}
