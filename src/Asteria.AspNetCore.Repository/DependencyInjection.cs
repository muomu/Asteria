using Asteria.Repository;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public static partial class DependencyInjection
    {
        /// <summary>
        /// 增加仓储
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepository(this IServiceCollection services, Action<RepositoryOption> optionAction)
        {
            var builder = new RepositoryOption() 
            {
                Services = services 
            };
            optionAction?.Invoke(builder);

            services.TryAddScoped<IEntityEventSource, DefaultEntityEventSource>();
            services.TryAddScoped<IQueryFilter, DefaultQueryFilter>();

            return services;
        }

    }
}
