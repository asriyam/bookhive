namespace BookHive.Core.Entities;

/// <summary>
/// Represents user activity (updates, actions) in BookHive.
/// </summary>
public class Activity
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string ActivityType { get; set; } = string.Empty; // e.g., "read", "rated", "reviewed"
    public string? Rating { get; set; }
    public string ActivityContent { get; set; } = string.Empty;
    public string? BookId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties (for future EF Core configuration)
    public User? User { get; set; }
    public Book? Book { get; set; }
}
