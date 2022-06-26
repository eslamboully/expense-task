using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
        Task<T> GetItemByIdAsync(string id);
        Task<IReadOnlyList<T>> GetItemsAsync();
        Task<T> GetItemWithSpecificAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetItemsWithSpecificAsync(ISpecification<T> spec);
        public IReadOnlyList<T> GetItemsWithSpecific(ISpecification<T> spec);
        public T GetItemWithSpecific(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task AddAsync(T model);
        void Remove(T model);
        void RemoveRange(List<T> model);
}