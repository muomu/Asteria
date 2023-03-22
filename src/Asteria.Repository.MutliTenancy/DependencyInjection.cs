using Asteria.MutliTenancy;
using Asteria.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// 为服务容器注册多租户服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMutliTenancy(this IServiceCollection services)
        {
            return AddMutliTenancy(services, option =>
            {
                option.WithIdentityClaim();
                option.WithHttpHeader();
            });
        }


        /// <summary>
        /// 为服务容器注册多租户服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="buildAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddMutliTenancy(this IServiceCollection services, Action<ITenantIdFactoryBuilder> buildAction)
        {
            InjectPrivateServices(services);

            services.TryAddTransient<ITenantIdFactoryBuilder, CompositeTenantIdFactoryBuilder>();
            services.TryAddSingleton(sp =>
            {
                var builder = sp.GetRequiredService<ITenantIdFactoryBuilder>();
                buildAction?.Invoke(builder);
                return builder.Build(sp);
            });

            return services;
        }

        /// <summary>
        /// 为服务容器注册多租户服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static IServiceCollection AddMutliTenancy(this IServiceCollection services, Func<HttpContext, ValueTask<string?>> factory)
        {
            InjectPrivateServices(services);

            services.TryAddSingleton((sp) => new LambdaTenantIdFactory(factory));
            return services;
        }

        private static void InjectPrivateServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.TryAddTransient<ITenantIdAccessor, TenantIdAccessor>();
            services.TryAddSingleton<MutliTenancyMiddleware>();
        }

        /// <summary>
        /// 通过HTTP上下文中指定的 http header 获取租户id
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="headerName"></param>
        /// <returns></returns>
        public static ITenantIdFactoryBuilder WithHttpHeader(this ITenantIdFactoryBuilder builder, string headerName = "x-http-tenantid")
        {
            builder.AddProvider(new HttpHeaderTenantIdProvider(headerName));
            return builder;
        }

        /// <summary>
        /// 通过HTTP上下文中指定的 identity claim 获取租户id
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static ITenantIdFactoryBuilder WithIdentityClaim(this ITenantIdFactoryBuilder builder, string claimType = "tenantid")
        {
            builder.AddProvider(new IdentityClaimTenantIdProvider(claimType));
            return builder;
        }
    

        /// <summary>
        /// 启用多租户支持
        /// </summary>
        /// <param name="option"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        public static RepositoryOption EnableMutliTenancy(this RepositoryOption option, bool enable = true)
        {
            var service = option.Services;
            foreach(var x in service)
            {
                if (x.ServiceType == typeof(EntityEventListener<>) && typeof(MutliTenantModelEntityListener<>).Equals(x.ImplementationType))
                {
                    service.Remove(x);
                }
            }
            foreach(var x in service)
            {
                if (x.ServiceType == typeof(IQueryFilterProvider<>) && typeof(MutliTenantModelQueryFilterProvider<>).Equals(x.ImplementationType))
                {
                    service.Remove(x);
                }
            }

            if (enable)
            {
                service.AddScoped(typeof(EntityEventListener<>), typeof(MutliTenantModelEntityListener<>));
                service.AddScoped(typeof(IQueryFilterProvider<>), typeof(MutliTenantModelQueryFilterProvider<>));
            }

            return option;
        }
    
    }
}
