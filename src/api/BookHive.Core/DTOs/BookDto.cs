namespace BookHive.Core.DTOs;

/// <summary>
/// Data Transfer Object for Book entity.
/// </summary>
public class BookDto
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string BookImageUrl { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public string ListName { get; set; } = string.Empty;
    public string ListNameEncoded { get; set; } = string.Empty;
}
