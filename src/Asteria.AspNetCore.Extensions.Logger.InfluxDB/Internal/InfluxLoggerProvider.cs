using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Asteria.Extensions.Logger.InfluxDB
{
    sealed class InfluxLoggerProvider : ILoggerProvider
    {
        public InfluxLoggerProvider(IInfluxWriter writer, string measurement)
        {
            _writer = writer;
            _measurement = measurement;
        }

        readonly ConcurrentDictionary<string, InfluxLogger> _loggers = new();
        readonly IInfluxWriter _writer;
        readonly string _measurement;

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, factory =>
            {
                var logger = new InfluxLogger(categoryName, _writer, _measurement);
                return logger;
            });
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
