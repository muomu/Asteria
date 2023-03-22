namespace Asteria.Repository
{
    /// <summary>
    /// 定义实体事件源的事件类型
    /// </summary>
    public enum EntityEventType
    {
        /// <summary>
        /// 执行了实体插入
        /// </summary>
        Added,
        /// <summary>
        /// 执行了实体修改
        /// </summary>
        Modified,
        /// <summary>
        /// 执行了实体删除
        /// </summary>
        Deleted
    }


}
