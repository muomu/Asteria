using System.Text.RegularExpressions;

namespace Asteria.Extensions.Logger.InfluxDB
{
    /// <inheritdoc/>
    public class TSDBConnection : ITSDBConnection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public TSDBConnection(string connectionString)
        {
            Match match = null!;

            match = Regex.Match(connectionString, @"endpoint=http[s]?://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?(:\d+)?[^;]", RegexOptions.IgnoreCase);
            if (match.Success)
                Endpoint = match.Value[9..];
            else
                throw new ArgumentException("无效的连接字符串，没有指定url地址");

            match = Regex.Match(connectionString, @"username=[^;]+", RegexOptions.IgnoreCase);
            if (match.Success)
                Username = match.Value[9..];
            else
                throw new ArgumentException("无效的连接字符串，没有指定用户名");

            match = Regex.Match(connectionString, @"password=[^;]+", RegexOptions.IgnoreCase);
            if (match.Success)
                Password = match.Value[9..];
            else
                throw new ArgumentException("无效的连接字符串，没有指定密码");

            match = Regex.Match(connectionString, @"database=[\D][^;]+", RegexOptions.IgnoreCase);
            if (match.Success)
                Database = match.Value[9..];
            else
                throw new ArgumentException("无效的连接字符串，没有指定数据库");

            match = Regex.Match(connectionString, @"maxbatch=[\d][^;]+", RegexOptions.IgnoreCase);
            if (match.Success)
                MaxBatch = int.Parse(match.Value[9..]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="database"></param>
        public TSDBConnection(string url, string username, string password, string database)
        {
            Endpoint = url;
            Username = username;
            Password = password;
            Database = database;
        }

        /// <inheritdoc/>
        public string Endpoint { get; }

        /// <inheritdoc/>
        public string Username { get; }

        /// <inheritdoc/>
        public string Password { get; }

        /// <inheritdoc/>
        public string Database { get; }

        /// <summary>
        /// 批处理
        /// </summary>
        public int MaxBatch { get; } = 20;
    }
}
