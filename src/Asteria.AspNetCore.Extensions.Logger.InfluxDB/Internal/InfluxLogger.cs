using Microsoft.Extensions.Logging;

namespace Asteria.Extensions.Logger.InfluxDB
{
    /// <summary>
    /// 表示使用时序时空数据库持久化的日志组件
    /// </summary>
    class InfluxLogger : ILogger
    {
        readonly string _categoryName;
        readonly LoggerExternalScopeProvider _scopeProvider = new();
        readonly IInfluxWriter _writer;
        readonly string _measurement;

        public InfluxLogger(string categoryName, IInfluxWriter workUnit, string measurement)
        {
            _categoryName = categoryName;
            _writer = workUnit;
            _measurement = measurement;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return _scopeProvider.Push(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string level = logLevel.ToString();
            string name = _categoryName;
            string message = formatter(state, exception);

            _writer.Write(_measurement, builder =>
            {
                builder
                .AddTag("EventLevel", level)
                .AddTag("EventName", name)
                .AddTag("EventId", eventId.Id)
                .AddField("EventLevel", level)
                .AddField("EventName", name)
                .AddField("Message", message);

                if (exception is not null)
                {
                    builder.AddField("Exception", exception.Message);
                }

            });

        }
    }
}
