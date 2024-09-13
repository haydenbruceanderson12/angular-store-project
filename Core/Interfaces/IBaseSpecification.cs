using System.Linq.Expressions;

namespace Core.Interfaces;

public interface IBaseSpecification<T>
{
    Expression<Func<T, bool>>? WhereClauseExpression { get; }
    Expression<Func<T, object>>? OrderyByAscending { get; }
    Expression<Func<T, object>>? OrderyByDescending { get; }
    bool IsDistinct { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
    IQueryable<T> ApplyCriteria(IQueryable<T> query);
}

public interface IBaseSpecification<T, TResult> : IBaseSpecification<T>
{
    Expression<Func<T, TResult>>? SelectClauseExpression { get; }
}