using Infrastructure.Models;
using Infrastructure.Specification;

namespace Infrastructure.Specifications;

public class UserSpecification : BaseSpecification<User>
{
    public UserSpecification(string email) : base(x => x.Email == email)
    {
        
    }
}