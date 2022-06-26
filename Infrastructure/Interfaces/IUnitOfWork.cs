using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> Repository<T>() where T : BaseEntity;
    int SaveChanges();
    Task<int> SaveChangesAsync();
}