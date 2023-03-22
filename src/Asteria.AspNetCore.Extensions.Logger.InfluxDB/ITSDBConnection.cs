namespace Asteria.Extensions.Logger.InfluxDB
{
    /// <summary>
    /// 提供时序时空数据库的连接参数
    /// </summary>
    public interface ITSDBConnection
    {
        /// <summary>
        /// 获取数据库Url地址
        /// </summary>
        string Endpoint { get; }
        /// <summary>
        /// 获取数据库登录用户名
        /// </summary>
        string Username { get; }
        /// <summary>
        /// 获取数据库登录密码
        /// </summary>
        string Password { get; }
        /// <summary>
        /// 获取连接的数据库名称
        /// </summary>
        string Database { get; }

        /// <summary>
        /// 获取最大批处理Point大小
        /// </summary>
        int MaxBatch { get; }
    }
}
