using Infrastructure.Models;
using Infrastructure.Specification;

namespace Infrastructure.Specifications;

public class ExpenseSpecification : BaseSpecification<Expense>
{
    public ExpenseSpecification(ExpenseSpecParams spec) : base(
        x =>  (spec.StartAt == DateTime.MinValue || x.ExpenseDate >= spec.StartAt) &&
                (spec.EndAt == DateTime.MinValue || x.ExpenseDate <= spec.EndAt)
    )
    {
        
    }
}