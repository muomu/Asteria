using InfluxData.Net.Common.Enums;
using InfluxData.Net.InfluxDb;
using InfluxData.Net.InfluxDb.Models;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;

namespace Asteria.Extensions.Logger.InfluxDB
{

    internal class InfluxWriter : BackgroundService, IInfluxWriter
    {
        readonly object sync_root = new();
        readonly ConcurrentBag<Point> _points = new();
        readonly string _database = null!;

        InfluxDbClient InfluxDbClient { get; }
        int MaximumBatchSize { get; }
        ConcurrentDictionary<string, object> GlobalTags { get; } = new ConcurrentDictionary<string, object>();
        ConcurrentDictionary<string, object> GlobalFields { get; } = new ConcurrentDictionary<string, object>();

        public int BatchSize => MaximumBatchSize;

        public int InternalMillisec { get; } = 3000;


        public InfluxWriter(ITSDBConnection connection, HttpClient httpClient)
        {
            InfluxDbClient = new InfluxDbClient(connection.Endpoint, connection.Username, connection.Password, InfluxDbVersion.Latest, QueryLocation.FormData, httpClient, false);
            MaximumBatchSize = connection.MaxBatch;
            _database = connection.Database;
        }

        public IInfluxWriter AddGlobalTag(string key, object value)
        {
            GlobalTags.TryAdd(key, value);
            return this;
        }

        public IInfluxWriter AddGlobalField(string key, object value)
        {
            GlobalFields.TryAdd(key, value);
            return this;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(InternalMillisec, stoppingToken);
                Flush();
            }
            Flush();
        }

        public void Write(string measurement, Action<InfluxPointBuilder> builderAction)
        {
            if (string.IsNullOrWhiteSpace(measurement)) throw new ArgumentNullException(nameof(measurement));
            if (builderAction == null) throw new ArgumentNullException(nameof(builderAction));

            InfluxPointBuilder builder = new();

            builderAction.Invoke(builder);

            Point point = builder.Build(measurement);

            foreach (var x in GlobalTags)
            {
                point.Tags?.TryAdd(x.Key, x.Value);
            }
            foreach (var x in GlobalFields)
            {
                point.Fields?.TryAdd(x.Key, x.Value);
            }

            _points.Add(point);
        }


        public void Flush()
        {
            lock (sync_root)
            {
                List<Point> points = new();
                while (_points.TryTake(out var item))
                {
                    points.Add(item);
                    if (points.Count >= MaximumBatchSize)
                    {
                        InfluxDbClient.Client.WriteAsync(points, _database);
                        points.Clear();
                    }
                }
                if (points.Count > 0)
                {
                    InfluxDbClient.Client.WriteAsync(points, _database);
                }
            }
        }
    }
}
