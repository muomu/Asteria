using Microsoft.AspNetCore.Mvc;

namespace Asteria.AspNetCore.Extensions
{
    /// <summary>
    /// 提供分页集合统一结构的封装
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public readonly struct Pagination<T>
    {
        /// <summary>
        /// 实例化分页集合对象
        /// </summary>
        /// <param name="items"></param>
        /// <param name="offset"></param>
        /// <param name="maximum"></param>
        public Pagination(ICollection<T> items, int offset, int maximum)
        {
            Items = items;
            Maximum = maximum;
            Offset = offset;
        }

        /// <summary>
        /// 获取返回的原始
        /// </summary>
        public ICollection<T> Items { get; }

        /// <summary>
        /// 获取返回的偏移量
        /// </summary>
        public int Offset { get; }

        /// <summary>
        /// 获取返回的最大数量
        /// </summary>
        public int Maximum { get; }


        /// <summary>
        /// 获取返回的集合大小
        /// </summary>
        public int Count => Items.Count;

        /// <summary>
        /// 获取返回的高度
        /// </summary>
        public int Highest => Offset + Count - 1;

        /// <summary>
        /// 获取返回的范围
        /// </summary>
        public Range Range => Offset..(Highest > 0 ? Highest : 0);

        /// <summary>
        /// 隐式转换为PaginationResult
        /// </summary>
        /// <param name="pagination"></param>
        public static implicit operator PaginationResult<T>(Pagination<T> pagination)
        {
            return new(pagination);
        }

        /// <summary>
        /// 隐式转换为ActionResult
        /// </summary>
        /// <param name="pagination"></param>
        public static implicit operator ActionResult(Pagination<T> pagination)
        {
            return new PaginationResult<T>(pagination);
        }


    }
}
