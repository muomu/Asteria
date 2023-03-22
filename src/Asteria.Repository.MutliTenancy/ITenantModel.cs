namespace Asteria.MutliTenancy
{
    /// <summary>
    /// 定义多租户模型体系结构支撑所需要的属性
    /// </summary>
    public interface ITenantModel
    {
        /// <summary>
        /// 获取或设置实体的租户id
        /// </summary>
        string TenantId { get; set; }
    }
}
