namespace Asteria.MutliTenancy
{
    /// <summary>
    /// 提供构建租户id工厂的抽象组织
    /// </summary>
    public interface ITenantIdFactoryBuilder
    {
        /// <summary>
        /// 增加租户id提供者
        /// </summary>
        /// <param name="provider"></param>
        void AddProvider(ITenantIdProvider provider);

        /// <summary>
        /// 清除所有租户id提供程序
        /// </summary>
        void ClearProvider();

        /// <summary>
        /// 构建租户id抽象工厂对象
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        ITenantIdFactory Build(IServiceProvider serviceProvider);
    }
}
