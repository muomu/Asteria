namespace System
{
    /// <summary></summary>
    public static class RangeExtension
    {
        /// <summary>
        /// 获取该范围的长度
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static int GetLength(this Range range)
        {
            return range.End.Value - range.Start.Value + 1;
        }

        /// <summary>
        /// 指定一个值，确定该值是否在指定的范围内
        /// </summary>
        /// <param name="range"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Contains(this Range range, int value)
        {
            return value >= range.Start.Value && value <= range.End.Value;
        }
    }
}
