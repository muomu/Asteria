using Microsoft.Extensions.DependencyInjection;

namespace Asteria.Repository
{
    class DefaultEntityEventSource : IEntityEventSource
    {
        public DefaultEntityEventSource(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        IServiceProvider ServiceProvider { get; }

        public ValueTask OnSaveAfterAsync(object entity, EntityEventType eventType, CancellationToken cancellationToken = default)
        {
            return Dispatch(entity, eventType, (l, e, t, c) => l.OnSaveAfterAsync(e, t, c), cancellationToken);
        }

        public ValueTask OnSaveBeforeAsync(object entity, EntityEventType eventType, CancellationToken cancellationToken = default)
        {
            return Dispatch(entity, eventType, (l, e, t, c) => l.OnSaveBeforeAsync(e, t, c), cancellationToken);
        }

        private async ValueTask Dispatch(object entity, EntityEventType eventType, Func<IEntityEventListener, object, EntityEventType, CancellationToken, ValueTask> func, CancellationToken cancellationToken)
        {
            var services = ServiceProvider;

            var type = typeof(EntityEventListener<>).MakeGenericType(entity.GetType());
            var listeners = services.GetServices(type);

            foreach (var x in listeners)
            {
                if (x is IEntityEventListener listener)
                {
                    await func(listener, entity, eventType, cancellationToken);
                }
            }

        }
    }
}
