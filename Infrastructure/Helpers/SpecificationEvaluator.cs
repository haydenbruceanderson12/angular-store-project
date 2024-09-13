using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Infrastructure.Helpers;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    /// <summary>
    /// Modifies the IQueryable<T> which will be the query
    /// sent to retrive data from the DB.
    /// </summary>
    /// <param name="query">The existing DB query.</param>
    /// <param name="specification">The specification of changes to the query.</param>
    /// <returns>
    /// The modified query to retrieve data from the DB.
    /// </returns>
    public static IQueryable<T> GetQuery(IQueryable<T> query, IBaseSpecification<T> specification)
    {
        if (specification.WhereClauseExpression is not null)
        {
            // eg: specification.WhereClauseExpression = (x => x.Prop == prop)
            query = query.Where(specification.WhereClauseExpression);
        }

        if (specification.OrderyByAscending is not null)
        {
            query.OrderBy(specification.OrderyByAscending);
        }

        if (specification.OrderyByDescending is not null)
        {
            query.OrderByDescending(specification.OrderyByDescending);
        }

        if (specification.IsDistinct)
        {
            query = query.Distinct();
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        return query;
    }

    public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query, 
        IBaseSpecification<T, TResult> specification)
    {
        if (specification.WhereClauseExpression is not null)
        {
            query = query.Where(specification.WhereClauseExpression);
        }

        if (specification.OrderyByAscending is not null)
        {
            query.OrderBy(specification.OrderyByAscending);
        }

        if (specification.OrderyByDescending is not null)
        {
            query.OrderByDescending(specification.OrderyByDescending);
        }

        var selectQuery = query as IQueryable<TResult>;

        if (specification.SelectClauseExpression is not null)
        {
            selectQuery = query.Select(specification.SelectClauseExpression);
        }

         if (specification.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }

        if (specification.IsPagingEnabled)
        {
            selectQuery = selectQuery?.Skip(specification.Skip).Take(specification.Take);
        }

        return selectQuery ?? query.Cast<TResult>();
    }
}
