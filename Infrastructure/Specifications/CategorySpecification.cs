using Infrastructure.Models;
using Infrastructure.Specification;

namespace Infrastructure.Specifications;

public class CategorySpecification : BaseSpecification<Category>
{
    public CategorySpecification(string name) : base(x => x.Name == name)
    {
        
    }
}