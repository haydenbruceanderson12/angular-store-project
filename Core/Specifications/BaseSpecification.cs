using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T> : IBaseSpecification<T>
{
    public BaseSpecification() { }

    private readonly Expression<Func<T, bool>>? _expression;

    public BaseSpecification(Expression<Func<T, bool>> Expression)
    {
        _expression = Expression;
    }

    public Expression<Func<T, bool>>? WhereClauseExpression => _expression;

    public Expression<Func<T, object>>? OrderyByAscending { get; private set; }

    public Expression<Func<T, object>>? OrderyByDescending { get; private set; }

    public bool IsDistinct { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    protected void AddOrderByAscending(Expression<Func<T, object>> expression)
    {
        OrderyByAscending = expression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> expression)
    {
        OrderyByDescending = expression;
    }

    protected void ApplyDistinctFilter()
    {
        IsDistinct = true;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }

    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if (WhereClauseExpression is not null)
        {
            query = query.Where(WhereClauseExpression);
        }

        return query;
    }
}

public class BaseSpecification<T, TResult> : BaseSpecification<T>, IBaseSpecification<T, TResult>
{
    public BaseSpecification() { }

    private readonly Expression<Func<T, bool>>? _expression;

    public BaseSpecification(Expression<Func<T, bool>> Expression)
    {
        _expression = Expression;
    }

    public Expression<Func<T, TResult>>? SelectClauseExpression { get; private set; }

    protected void AddSelectClause(Expression<Func<T, TResult>> expression)
    {
        SelectClauseExpression = expression;
    }
}