using Asteria.Repository;
using Asteria.Repository.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// 使用 EntityFrameworkCore DbContext
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static RepositoryOption UseEntityFrameworkCore<TDbContext>(this RepositoryOption builder) where TDbContext : DbContext
        {
            //builder.Services.RemoveAll<IUnitOfWork>();
            //builder.Services.RemoveAll(typeof(IRepository<>));

            builder.Services.TryAddScoped<IUnitOfWork>(sp => new EntityFrameworkUnitOfWork<TDbContext>(sp.GetRequiredService<TDbContext>(), sp.GetRequiredService<IEntityEventSource>(), sp.GetRequiredService<IQueryFilter>()));
            builder.Services.TryAddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));

            return builder;
        }
    }

}
