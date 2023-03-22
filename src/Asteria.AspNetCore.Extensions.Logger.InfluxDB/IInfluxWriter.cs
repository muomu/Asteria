using Microsoft.Extensions.Hosting;

namespace Asteria.Extensions.Logger.InfluxDB
{
    /// <summary>
    /// 写入Influx
    /// </summary>
    public interface IInfluxWriter : IHostedService, IDisposable
    {
        /// <summary>
        /// 增加全局字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IInfluxWriter AddGlobalField(string key, object value);
        /// <summary>
        /// 增加全局Tag
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IInfluxWriter AddGlobalTag(string key, object value);
        /// <summary>
        /// 刷新缓冲区
        /// </summary>
        void Flush();
        /// <summary>
        /// 写入Point
        /// </summary>
        /// <param name="measurement"></param>
        /// <param name="builderAction"></param>
        void Write(string measurement, Action<InfluxPointBuilder> builderAction);
    }
}
