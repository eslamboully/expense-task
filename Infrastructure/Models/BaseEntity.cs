namespace Infrastructure.Models;

public class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}