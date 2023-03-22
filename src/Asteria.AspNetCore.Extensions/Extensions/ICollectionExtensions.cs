namespace System.Collections.Generic
{
    /// <summary></summary>
    public static class ICollectionExtensions
    {
        /// <summary>
        /// 获取一个值，该值指示该集合是否是一个空集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count <= 0;
        }
    }
}
