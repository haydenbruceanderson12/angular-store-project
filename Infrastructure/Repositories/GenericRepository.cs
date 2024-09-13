using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// This repository will only 'allow' entities deriving 
/// from the BaseEntity - (where T ... clause).
/// 
/// The purpose of this class is to create an implementation
/// for our generic repository which will be responsible for 
/// data access.
/// 
/// The type of T that is passed in as a TypeParam, is
/// also implicitly the return type*.
/// </summary>
/// <typeparam name="T">Generic type 'place-holder', eg: Product</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreDbContext _storeDbContext;

    public GenericRepository(StoreDbContext storeDbContext)
    {
        _storeDbContext = storeDbContext;
    }

    public void Create(T entity)
    {
        // non-generic eg: _storeDbContext.Products.Add(product);
        _storeDbContext.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        // non-generic eg: _storeDbContext.Products.Remove(product);
        _storeDbContext.Set<T>().Remove(entity);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        // non-generic eg: await _storeDbContext.Products.ToListAsync();
        return await _storeDbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetEntityByIdAsync(int id)
    {
        // non-generic eg: await _storeDbContext.Products.FindAsync(id);
        return await _storeDbContext.Set<T>().FindAsync(id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _storeDbContext.SaveChangesAsync() > 0;
    }

    public void Update(T entity)
    {
        // non-generic eg: _storeDbContext.Products.Update(product);
        _storeDbContext.Set<T>().Update(entity);
    }

    public bool Exists(int id)
    {
        // non-generic eg: _storeDbContext.Products.Any();
        return _storeDbContext.Set<T>().Any(x => x.Id == id);
    }

    public async Task<T?> GetEntityWithSpecificationAsync(IBaseSpecification<T> specification)
    {
        // non-generic eg: await _storeDbContext.Products.Where(x => x.Prop == prop).FirstOrDefaultAsync();
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(IBaseSpecification<T> specification)
    {
        // non-generic eg: await _storeDbContext.Products.Where(x => x.Prop == prop).ToListAsync();
        return await ApplySpecification(specification).ToListAsync();
    }

    public async Task<TResult?> GetEntityWithSpecificationAsync<TResult>(IBaseSpecification<T, TResult> specification)
    {
        // non-generic eg: await _storeDbContext.Products.Select(x => x.Prop).Where(x => x.Prop == prop).FirstOrDefaultAsync();
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TResult>> GetAllWithSpecificationAsync<TResult>(IBaseSpecification<T, TResult> specification)
    {
        // non-generic eg: await _storeDbContext.Products.Select(x => x.Prop).Where(x => x.Prop == prop).ToListAsync();
        return await ApplySpecification(specification).ToListAsync();
    }

     private IQueryable<T> ApplySpecification(IBaseSpecification<T> specification)
    {
        // non-generic eg: _storeDbContext.Products;
        var queryable = _storeDbContext.Set<T>().AsQueryable();

        // non-generic eg: _storeDbContext.Products.Where(x => x.Prop == prop);
        return SpecificationEvaluator<T>.GetQuery(queryable, specification);
    }

     private IQueryable<TResult> ApplySpecification<TResult>(IBaseSpecification<T, TResult> specification)
    {
        // non-generic eg: _storeDbContext.Products;
        var queryable = _storeDbContext.Set<T>().AsQueryable();

        // non-generic eg: _storeDbContext.Products.Where(x => x.Prop == prop);
        return SpecificationEvaluator<T>.GetQuery<T, TResult>(queryable, specification);
    }

    // TODO!
    public async Task<int> CountAsync(IBaseSpecification<T> specification)
    {
        var query = _storeDbContext.Set<T>().AsQueryable();

        query = specification.ApplyCriteria(query);

        return await query.CountAsync();
    }
}
