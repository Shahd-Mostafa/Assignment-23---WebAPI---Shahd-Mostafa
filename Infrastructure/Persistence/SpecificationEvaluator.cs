using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(this IQueryable<T> inputQuery, Specifications<T> specs) where T : class
        {
            var query = inputQuery;
            if (specs.Criteria != null)
            {
                query = query.Where(specs.Criteria);
            }
            if (specs.Include.Any())
            {
                query= specs.Include.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));
            }
            if (specs.OrderBy != null)
            {
                query = query.OrderBy(specs.OrderBy);
            }
            if (specs.OrderByDescending != null)
            {
                query = query.OrderByDescending(specs.OrderByDescending);
            }
            if (specs.IsPaginated)
            {
                query = query.Skip(specs.Skip).Take(specs.Take);
            }
            return query;
        }
    }
}
