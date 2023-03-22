namespace Asteria
{
    /// <summary>
    /// 树形结构返回体
    /// </summary>
    public class TreeObject<TKey, TItem> where TItem : TreeObject<TKey, TItem>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public TKey Id { get; set; } = default!;

        /// <summary>
        /// 父对象的主键
        /// </summary>
        public TKey Parent { get; set; } = default!;

        /// <summary>
        /// 子元素
        /// </summary>
        public List<TItem> Children { get; set; } = new List<TItem>();

        /// <summary>
        /// 子元素数量
        /// </summary>
        public int Totals => Children.Count;


        /// <summary>
        /// 生成树
        /// </summary>
        /// <param name="items"></param>
        public void GenerateTree(IEnumerable<TItem> items)
        {
            GenerateTree(Id, Children, items);
        }

        private static void GenerateTree(TKey id, List<TItem> children, IEnumerable<TItem> items)
        {
            foreach (var x in items)
            {
                if (x.Parent!.Equals(id))
                {
                    children.Add(x);
                    GenerateTree(x.Id, x.Children, items);
                }
            }
        }
    }
}
