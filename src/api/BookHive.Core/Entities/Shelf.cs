namespace BookHive.Core.Entities;

/// <summary>
/// Represents a book shelf (category) in BookHive.
/// </summary>
public class Shelf
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public long Count { get; set; }
    public bool IsCurated { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
