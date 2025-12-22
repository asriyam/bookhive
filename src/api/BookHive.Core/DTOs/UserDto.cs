namespace BookHive.Core.DTOs;

/// <summary>
/// Data Transfer Object for User entity.
/// </summary>
public class UserDto
{
    public string Id { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public string ProfileUrl { get; set; } = string.Empty;
    public string? Location { get; set; }
}
