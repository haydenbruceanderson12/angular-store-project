using Core.Entities;

namespace Core.Interfaces;

/// <summary>
/// This repository will only 'allow' entities deriving 
/// from the BaseEntity - (where clause).
/// 
/// The purpose of this interface is to create a contract
/// for our generic repository which will be responsible for 
/// data access.
/// </summary>
/// <typeparam name="T">Generic type 'place-holder', eg: Product</typeparam>
public interface IGenericRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Get an entity of type T given the id 
    /// and typeparam => (any entity, eg: Product). 
    /// </summary>
    /// <param name="id">Base entity identfier.</param>
    /// <returns>
    /// The entity of the type param passed (eg: Product)
    /// </returns>
    Task<T?> GetEntityByIdAsync(int id);

    /// <summary>
    /// TODO: Need to understand before commenting.
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    Task<T?> GetEntityWithSpecificationAsync(IBaseSpecification<T> specification);

    Task<TResult?> GetEntityWithSpecificationAsync<TResult>(IBaseSpecification<T, TResult> specification);

    /// <summary>
    /// Get an a list of entities of type T given the  
    /// typeparam => (any entity, eg: Product). 
    /// </summary>
    /// <returns>
    /// A readonly list of entities of the type param passed (eg: Product). 
    /// </returns>
    Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="specification"></param>
    /// <returns></returns>
    Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(IBaseSpecification<T> specification);

    Task<IReadOnlyList<TResult>> GetAllWithSpecificationAsync<TResult>(IBaseSpecification<T, TResult> specification);

    void Create(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task<bool> SaveChangesAsync();

    bool Exists(int id);

    Task<int> CountAsync(IBaseSpecification<T> specification);
}