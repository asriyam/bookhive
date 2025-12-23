using System.Text.Json.Serialization;

namespace BookHive.Core.Entities;

/// <summary>
/// Represents a book in the BookHive catalog.
/// </summary>
public class Book
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("book_image")]
    public string BookImageUrl { get; set; } = string.Empty;

    [JsonPropertyName("author")]
    public string Author { get; set; } = string.Empty;

    [JsonPropertyName("publisher")]
    public string Publisher { get; set; } = string.Empty;

    [JsonPropertyName("list_name")]
    public string ListName { get; set; } = string.Empty;

    [JsonPropertyName("list_name_encoded")]
    public string ListNameEncoded { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
