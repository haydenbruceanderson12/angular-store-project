using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetEntityByIdAsync(int id);
    Task<T?> GetEntityWithSpecificationAsync(IBaseSpecification<T> specification);
    Task<TResult?> GetEntityWithSpecificationAsync<TResult>(IBaseSpecification<T, TResult> specification);

    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(IBaseSpecification<T> specification);
    Task<IReadOnlyList<TResult>> GetAllWithSpecificationAsync<TResult>(IBaseSpecification<T, TResult> specification);

    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);

    Task<bool> SaveChangesAsync();
    
    bool Exists(int id);
    Task<int> CountAsync(IBaseSpecification<T> specification);
}