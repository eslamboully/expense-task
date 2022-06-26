using System.ComponentModel.DataAnnotations;
using Infrastructure.Models;

namespace Infrastructure.ViewModels;

public class ExpenseViewModel
{
    public ExpenseViewModel()
    {
    }

    public ExpenseViewModel(IReadOnlyList<Category> categories, IReadOnlyList<Expense> expenses)
    {
        Categories = categories;
        Expenses = expenses;
    }

    public IReadOnlyList<Category> Categories { get; set; }
    public IReadOnlyList<Expense> Expenses { get; set; }
}