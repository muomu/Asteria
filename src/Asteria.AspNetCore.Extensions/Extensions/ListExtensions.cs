namespace System.Collections.Generic
{
    /// <summary>
    /// IList对象扩展
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// 取出当前集合中的元素，并立即在集合中删除它
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T TakeOff<T>(this IList<T> list, int index)
        {
            var o = list[index];
            list.RemoveAt(index);
            return o;
        }


    }
}
