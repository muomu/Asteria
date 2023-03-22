using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Asteria.Extensions.Logger.InfluxDB
{
    /// <summary>
    /// 日志构建器扩展
    /// </summary>
    public static partial class DependencyInjection
    {
        static IInfluxWriter __writer = null!;

        /// <summary>
        /// 使用时序时空数据库持久化的日志器
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="serviceName">设置日志提供者标识</param>
        /// <param name="connectionString">设置时序时空数据库连接字符串</param>
        /// <returns></returns>
        public static ILoggingBuilder AddInflux(this ILoggingBuilder builder, string serviceName, string connectionString)
        {
            if (__writer is null)
            {
                ITSDBConnection connection = new TSDBConnection(connectionString);
                HttpClient httpClient = new(new SocketsHttpHandler
                {
                    ConnectTimeout = TimeSpan.FromSeconds(10),
                    EnableMultipleHttp2Connections = true,
                    MaxConnectionsPerServer = 1,
                    AutomaticDecompression = System.Net.DecompressionMethods.All
                })
                {
                    DefaultRequestVersion = Version.Parse("2.0")
                };
                __writer = new InfluxWriter(connection, httpClient);
                builder.Services.AddHostedService(sp => __writer);
                InfluxLoggerProvider influxLoggerProvider = new(__writer, serviceName);
                builder.AddProvider(influxLoggerProvider);
            }


            return builder;
        }

    }
}
