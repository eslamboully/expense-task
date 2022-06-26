using System.Collections;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;
    private Hashtable _repositories;

    public UnitOfWork(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IGenericRepository<T> Repository<T>() where T : BaseEntity
    {
        if (_repositories == null) _repositories = new Hashtable();
        
        var type = typeof(T).Name;
        
        if (!_repositories.ContainsKey(type))
        {
            var responseType = Activator.CreateInstance(typeof(GenericRepository<T>) , _context);
            _repositories.Add(type, responseType);
        }

        return (IGenericRepository<T>)_repositories[type];
    }
}