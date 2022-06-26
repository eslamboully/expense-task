using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class Category : BaseEntity
{
    [Required]
    [StringLength(180, MinimumLength = 3)]
    public string Name { get; set; }
}