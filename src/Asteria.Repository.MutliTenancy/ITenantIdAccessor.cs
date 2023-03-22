namespace Asteria.MutliTenancy
{
    /// <summary>
    /// 租户上下文
    /// </summary>
    public interface ITenantIdAccessor
    {

        /// <summary>
        /// 指示当前上下文是否存在租户的值
        /// </summary>
        bool IsTenant => TenantId is not null;

        /// <summary>
        /// 获取租户id
        /// </summary>
        string? TenantId { get; }
    }
}
