namespace BookHive.Core.DTOs;

/// <summary>
/// Data Transfer Object for Activity entity.
/// </summary>
public class ActivityDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string ActivityType { get; set; } = string.Empty;
    public string? Rating { get; set; }
    public string ActivityContent { get; set; } = string.Empty;
    public string? BookId { get; set; }
    public DateTime Timestamp { get; set; }
    public UserDto? User { get; set; }
    public BookDto? Book { get; set; }
}
