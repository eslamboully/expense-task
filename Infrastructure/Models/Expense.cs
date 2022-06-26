using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class Expense : BaseEntity
{
    [Required]
    public string CategoryId { get; set; }
    [Required]
    [StringLength(180, MinimumLength = 3)]
    public string Name { get; set; }
    [Required]
    [Range(1, Double.MaxValue, ErrorMessage = "The Amount must be integer or double number")]
    public double? Amount { get; set; }
    [Required]
    public DateTime? ExpenseDate { get; set; }
}