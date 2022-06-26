using System.Linq.Expressions;
using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface ISpecification<T> where T : BaseEntity
{
    Expression<Func<T,bool>> Criteria { get; set; }
    List<Expression<Func<T, object>>> Includes { get; set; }
    Expression<Func<T, object>> OrderByDesc { get; set; }
    Expression<Func<T, object>> OrderBy { get; set; }
    int Take { get; set; }
    int Skip { get; set; }
    bool IsPagingEnabled { get; }
}