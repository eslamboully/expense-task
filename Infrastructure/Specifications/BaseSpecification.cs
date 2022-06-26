using System.Linq.Expressions;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Specification;

public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
{
    public BaseSpecification()
    {
    }

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
    public Expression<Func<T, object>> OrderByDesc { get; set; }
    public Expression<Func<T, object>> OrderBy { get; set; }
    public int Take { get; set; }
    public int Skip { get; set; }
    public bool IsPagingEnabled { get; set; }

    protected void AddInclude(Expression<Func<T, object>> include)
    {
        Includes.Add(include);
    }

    protected void AddOrderBy(Expression<Func<T, object>> orderBy)
    {
        OrderBy = orderBy;
    }
    
    protected void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
    {
        OrderByDesc = orderByDesc;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}