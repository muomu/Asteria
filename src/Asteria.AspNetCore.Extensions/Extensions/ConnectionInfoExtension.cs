namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConnectionInfoExtension
    {
        /// <summary>
        /// 获取真实IP地址(ipv4)
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static string GetIPV4AddressString(this ConnectionInfo connection)
        {
            if (connection.RemoteIpAddress is not null)
            {
                return connection.RemoteIpAddress.MapToIPv4().ToString();
            }
            return string.Empty;
        }
    }
}
