using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

/// <summary>
/// This Base class should be used as the Parent class for each
/// Specification created. 
/// 
/// It handles the addition of common LINQ & EF operations, including
/// but not limited to the Where() & OrderBy() clauses.
/// 
/// The addition of the clauses are specific to the *Specification.
/// 
/// The SpecificationEvaluator class will evaluate the Specification
/// and return the relevant IQueryable.
/// </summary>
/// <typeparam name="T">The type of object to work with.</typeparam>
public class BaseSpecification<T> : IBaseSpecification<T>
{
    /// <summary>
    /// Instantiate the BaseSpecification class without a Where()
    /// clause expression - no filtering of the result set.
    /// </summary>
    public BaseSpecification() { }

    private readonly Expression<Func<T, bool>>? _expression;

    /// <summary>
    /// Add a Where() clause to the *Specification.
    /// </summary>
    /// <param name="expression">The expression to filter by.</param>
    public BaseSpecification(Expression<Func<T, bool>> expression)
    {
        _expression = expression;
    }

    /// <summary>
    /// Returns a relevant Where() clause or filter expression.
    /// </summary>
    public Expression<Func<T, bool>>? WhereClauseExpression => _expression;

    /// <summary>
    /// Holds the expression value of the OrderBy() clause.
    /// To set this value, call the AddOrderByAscending() method, with the relevant expression.
    /// </summary>
    public Expression<Func<T, object>>? OrderyByAscending { get; private set; }

    /// <summary>
    /// Holds the expression value of the OrderByDescending() clause.
    /// To set this value, call the AddOrderByDescending() method, with the relevant expression.
    /// </summary>
    public Expression<Func<T, object>>? OrderyByDescending { get; private set; }

    /// <summary>
    /// Holds the value of the IsDistinct flag.
    /// To set this flag to true, use the ApplyDistinctFilter() method.
    /// </summary>
    public bool IsDistinct { get; private set; }

    /// <summary>
    /// Holds the value of the amount/number of objects to take when using pagination.
    /// </summary>
    public int Take { get; private set; }

    /// <summary>
    /// Holds the value of the amount/number of objects to skip when using pagination.
    /// </summary>
    public int Skip { get; private set; }

    /// <summary>
    /// Holds the value of the IsPagingEnabled flag.
    /// To set this flag to true, use the ApplyPaging() method and pass it the skip and take params.
    /// </summary>
    public bool IsPagingEnabled { get; private set; }

    /// <summary>
    /// Add an OrderBy() clause to the *Specification, using a LINQ expression.
    /// </summary>
    /// <param name="expression">The expression to order by.</param>
    protected void AddOrderByAscending(Expression<Func<T, object>> expression)
    {
        OrderyByAscending = expression;
    }

    /// <summary>
    /// Add an OrderByDescending() clause to the *Specification, using a LINQ expression.
    /// </summary>
    /// <param name="expression">The expression to order by.</param>
    protected void AddOrderByDescending(Expression<Func<T, object>> expression)
    {
        OrderyByDescending = expression;
    }

    /// <summary>
    /// Add a Distinct() clause to the *Specification.
    /// </summary>
    protected void ApplyDistinctFilter()
    {
        IsDistinct = true;
    }

    /// <summary>
    /// Add pagination to the *Specification.
    /// </summary>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to take.</param>
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }

    /// <summary>
    /// Add the Where() clause to the passed query.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>
    /// The query with the Where() clause added.
    /// </returns>
    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        return WhereClauseExpression is not null ? query = query.Where(WhereClauseExpression) : query;
    }
}

/// <summary>
/// This Base class should be used as the Parent class for each
/// Specification created. 
/// 
/// It handles the addition of common LINQ & EF operations, including
/// but not limited to the Where() & OrderBy() clauses.
/// 
/// The addition of the clauses are specific to the *Specification.
/// 
/// The SpecificationEvaluator class will evaluate the Specification
/// and return the relevant IQueryable.
/// </summary>
/// <typeparam name="T">The type of object to work with.</typeparam>
/// <typeparam name="TResult">The type of object to return.</typeparam>
public class BaseSpecification<T, TResult> : BaseSpecification<T>, IBaseSpecification<T, TResult>
{
    /// <summary>
    /// Instantiate the BaseSpecification class without a Where()
    /// clause expression - no filtering of the result set.
    /// </summary>
    public BaseSpecification() { }

    private readonly Expression<Func<T, bool>>? _expression;

    /// <summary>
    /// Add a Where() clause to the *Specification.
    /// </summary>
    /// <param name="expression">The expression to filter by.</param>
    public BaseSpecification(Expression<Func<T, bool>> expression)
    {
        _expression = expression;
    }

    /// <summary>
    /// Holds the expression value of the Select() clause.
    /// To set this value, call the AddSelectClause() method, with the relevant expression.
    /// </summary>
    public Expression<Func<T, TResult>>? SelectClauseExpression { get; private set; }

    /// <summary>
    /// Add a Select() clause to the *Specification, using a LINQ expression.
    /// </summary>
    /// <param name="expression">The expression to use as the Select.</param>
    protected void AddSelectClause(Expression<Func<T, TResult>> expression)
    {
        SelectClauseExpression = expression;
    }
}