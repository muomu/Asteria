namespace Asteria.AspNetCore.Extensions
{

    /// <summary>
    /// 排序信息
    /// </summary>
    public readonly struct SortColumn
    {
        private const string DescText = "~";

        /// <summary>
        /// 空
        /// </summary>
        public static SortColumn Empty { get; } = new SortColumn { FieldName = string.Empty, Inversed = false };

        /// <summary>
        /// 将要进行排序的字段名称
        /// </summary>
        public string FieldName { get; init; }

        /// <summary>
        /// 是否是反序
        /// </summary>
        public bool Inversed { get; init; }

        /// <summary>
        /// 返回SortInfo的字符串格式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(FieldName)) return string.Empty;

            if (Inversed)
                return DescText + FieldName;
            else
                return FieldName;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is SortColumn r)
            {
                return GetHashCode() == r.GetHashCode();
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            string str = this;
            return str.GetHashCode();
        }

        /// <summary>
        /// 将String显示转换为SortInfo对象
        /// </summary>
        /// <param name="str"></param>
        public static implicit operator SortColumn(string str)
        {
            str = str.Trim();
            if (string.IsNullOrWhiteSpace(str))
            {
                return Empty;
            }

            bool inversed = false;
            if (str.StartsWith(DescText))
            {
                inversed = true;
                str = str.Replace(DescText, "");
            }


            return new SortColumn
            {
                FieldName = str,
                Inversed = inversed
            };
        }

        /// <summary>
        /// 将SortInfo隐式转换为String对象
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator string(SortColumn value)
        {
            return value.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(SortColumn left, SortColumn right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(SortColumn left, SortColumn right)
        {
            return !left.Equals(right);
        }
    }
}
