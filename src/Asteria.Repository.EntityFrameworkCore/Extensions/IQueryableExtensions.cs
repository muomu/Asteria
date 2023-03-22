namespace System.Linq
{
    /// <summary>
    /// IQueryable 方法扩展
    /// </summary>
    public static class IQueryableExtensions
    {

        ///// <summary>
        ///// 分页返回数据
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="entities"></param>
        ///// <param name="offset">偏移量</param>
        ///// <param name="count">实体数量</param>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        //public static async ValueTask<Pagination<T>> ToPaginationAsync<T>(this IQueryable<T> entities, int offset, int count, CancellationToken cancellationToken = default)
        //{
        //    var result = await entities.Skip(offset).Take(count).ToArrayAsync(cancellationToken);
        //    var max = await entities.CountAsync(cancellationToken);

        //    return new Pagination<T>(result, offset, max);
        //}

        ///// <summary>
        ///// 分页获取数据
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="entities"></param>
        ///// <param name="range">范围</param>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        //public static async ValueTask<Pagination<T>> ToPaginationAsync<T>(this IQueryable<T> entities, Range range, CancellationToken cancellationToken = default)
        //{
        //    var max = await entities.CountAsync(cancellationToken);

        //    if (range.Equals(Range.All))
        //    {
        //        var result = await entities.ToArrayAsync(cancellationToken);
        //        return new Pagination<T>(result, 0, max);
        //    }
        //    else
        //    {
        //        var offset = range.Start.Value;
        //        var count = range.GetLength();

        //        var result = await entities.Skip(offset).Take(count).ToArrayAsync(cancellationToken);
        //        return new Pagination<T>(result, offset, max);
        //    }
            
        //}
    }
}
