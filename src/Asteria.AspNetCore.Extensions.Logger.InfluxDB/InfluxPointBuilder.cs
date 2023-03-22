using InfluxData.Net.InfluxDb.Models;
using System.Collections.Concurrent;

namespace Asteria.Extensions.Logger.InfluxDB
{
    /// <summary>
    /// 
    /// </summary>
    public class InfluxPointBuilder
    {
        readonly ConcurrentDictionary<string, object> _tags = new();
        readonly ConcurrentDictionary<string, object> _fields = new();

        /// <summary>
        /// 添加Tag
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public InfluxPointBuilder AddTag(string name, object value)
        {
            _tags.TryAdd(name, value);
            return this;
        }

        /// <summary>
        /// 增加值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public InfluxPointBuilder AddField(string name, object value)
        {
            _fields.TryAdd(name, value);
            return this;
        }

        internal Point Build(string measurement)
        {
            Point point = new()
            {
                Fields = _fields,
                Tags = _tags,
                Timestamp = DateTime.UtcNow,
                Name = measurement
            };
            return point;
        }
    }
}
