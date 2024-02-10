namespace TaskManager.Abstractions;
public class BaseEntity
{
    [Key]
    public int Id { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public DateTime? CreatedAt { get; set; }
}
