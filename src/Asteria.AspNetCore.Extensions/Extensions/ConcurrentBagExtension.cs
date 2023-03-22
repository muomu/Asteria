namespace System.Collections.Concurrent
{
    /// <inheritdoc/>
    public static class ConcurrentBagExtension
    {
        /// <summary>
        /// 以线程安全的方式取出指定数量的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bag"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<T> Extract<T>(this ConcurrentBag<T> bag, int count)
        {
            int p = 0;
            while (p++ < count && bag.TryTake(out var item))
            {
                yield return item;
            }
            yield break;
        }

        /// <summary>
        /// 以线程安全的方式取出该对象的所有元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bag"></param>
        /// <returns></returns>
        public static IEnumerable<T> ExtractAll<T>(this ConcurrentBag<T> bag)
        {
            while (bag.TryTake(out var item)) yield return item;
            yield break;
        }
    }
}
