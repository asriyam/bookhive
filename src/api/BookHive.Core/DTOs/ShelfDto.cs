namespace BookHive.Core.DTOs;

/// <summary>
/// Data Transfer Object for Shelf entity.
/// </summary>
public class ShelfDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public long Count { get; set; }
    public bool IsCurated { get; set; }
}
