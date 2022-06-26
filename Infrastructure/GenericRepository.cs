using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DatabaseContext _context;
    public GenericRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<T> GetItemByIdAsync(string id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetItemsAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetItemsWithSpecificAsync(ISpecification<T> spec)
    {
        return await ApplySpecifications(spec).ToListAsync();
    }

    public async Task<T> GetItemWithSpecificAsync(ISpecification<T> spec)
    {
        return await ApplySpecifications(spec).FirstOrDefaultAsync();
    }

    public IReadOnlyList<T> GetItemsWithSpecific(ISpecification<T> spec)
    {
        return ApplySpecifications(spec).ToList();
    }

    public T GetItemWithSpecific(ISpecification<T> spec)
    {
        return ApplySpecifications(spec).FirstOrDefault();
    }

    public async Task<int> CountAsync(ISpecification<T> spec) 
    {
        return await ApplySpecifications(spec).CountAsync();
    }

    public IQueryable<T> ApplySpecifications(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.SetQuery(_context.Set<T>(),spec);
    }

    public async Task AddAsync(T model)
    {
        await _context.Set<T>().AddAsync(model);
    }

    public void Remove(T model)
    {
        _context.Set<T>().Remove(model);
    }

    public void RemoveRange(List<T> listOfModels)
    {
        _context.Set<T>().RemoveRange(listOfModels);
    }
}