namespace Asteria.MutliTenancy
{
    /// <summary>
    /// 因多租户问题引发的异常
    /// </summary>
    public class TenantException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public TenantException(string message) : base(message)
        {

        }
    }
}
