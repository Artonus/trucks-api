using System.Linq.Expressions;
using Domain;

namespace TrucksApi.ExtensionMethods
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByPropertyName<T>(this IQueryable<T> q, SortingModel sort)
        {
            if (sort == null) throw new ArgumentNullException();
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, sort.SortFileld!);
            var exp = Expression.Lambda(prop, param);
            string method = sort.Ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var rs = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(rs);
        }
    }
}
