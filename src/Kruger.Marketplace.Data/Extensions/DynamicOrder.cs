using System.Linq.Expressions;

namespace Kruger.Marketplace.Data.Extensions
{
    public static class DynamicOrder
    {
        public static IOrderedQueryable<TSource> OrderByCustom<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, bool descending = false)
        {
            return descending ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
        }
    }
}
