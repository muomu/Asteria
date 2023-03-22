namespace Asteria
{
    /// <summary>
    /// 表示用户标识符
    /// </summary>
    public readonly struct UserIdentifier
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameIdentifier"></param>
        public UserIdentifier(string nameIdentifier)
        {
            _raw = nameIdentifier;
        }

        private readonly string _raw;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return _raw.Equals(obj?.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _raw.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _raw;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(UserIdentifier left, UserIdentifier right)
        {
            return left._raw == right._raw;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(UserIdentifier left, UserIdentifier right)
        {
            return left._raw != right._raw;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameIdentifier"></param>
        public static implicit operator string(UserIdentifier nameIdentifier)
        {
            return nameIdentifier._raw;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameIdentifier"></param>
        public static implicit operator int(UserIdentifier nameIdentifier)
        {
            if (!int.TryParse(nameIdentifier._raw, out var v))
            {
                throw new InvalidCastException($"指定的NameIdentifier值 '{nameIdentifier._raw}' 无法转换为Int");
            }

            return v;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameIdentifier"></param>
        public static implicit operator Guid(UserIdentifier nameIdentifier)
        {
            if (!Guid.TryParse(nameIdentifier._raw, out var v))
            {
                throw new InvalidCastException($"指定的NameIdentifier值 '{nameIdentifier._raw}' 无法转换为Guid");
            }

            return v;
        }
    }
}
