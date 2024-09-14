using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T>(StoreDbContext storeDbContext) : IGenericRepository<T> where T : BaseEntity
{
    public void Create(T entity)
    {
        storeDbContext.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        storeDbContext.Set<T>().Remove(entity);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await storeDbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetEntityByIdAsync(int id)
    {
        return await storeDbContext.Set<T>().FindAsync(id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await storeDbContext.SaveChangesAsync() > 0;
    }

    public void Update(T entity)
    {
        storeDbContext.Set<T>().Update(entity);
    }

    public bool Exists(int id)
    {
        return storeDbContext.Set<T>().Any(x => x.Id == id);
    }

    public async Task<T?> GetEntityWithSpecificationAsync(IBaseSpecification<T> specification)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(IBaseSpecification<T> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }

    public async Task<TResult?> GetEntityWithSpecificationAsync<TResult>(IBaseSpecification<T, TResult> specification)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TResult>> GetAllWithSpecificationAsync<TResult>(IBaseSpecification<T, TResult> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }
    
    public async Task<int> CountAsync(IBaseSpecification<T> specification)
    {
        var query = storeDbContext.Set<T>().AsQueryable();

        query = specification.ApplyCriteria(query);

        return await query.CountAsync();
    }

    private IQueryable<T> ApplySpecification(IBaseSpecification<T> specification)
    {
        var queryable = storeDbContext.Set<T>().AsQueryable();

        return SpecificationEvaluator<T>.GetQuery(queryable, specification);
    }

    private IQueryable<TResult> ApplySpecification<TResult>(IBaseSpecification<T, TResult> specification)
    {
        var queryable = storeDbContext.Set<T>().AsQueryable();

        return SpecificationEvaluator<T>.GetQuery<T, TResult>(queryable, specification);
    }
}
