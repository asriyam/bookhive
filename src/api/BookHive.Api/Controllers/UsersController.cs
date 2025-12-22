using BookHive.Core.DTOs;
using BookHive.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookHive.Api.Controllers;

/// <summary>
/// API controller for user-related operations.
/// Provides endpoints for retrieving user information and user-specific data.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController(MockDataService mockDataService) : ControllerBase
{
    /// <summary>
    /// Gets a specific user by ID.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <returns>The user details or 404 if not found.</returns>
    /// <response code="200">Returns the user.</response>
    /// <response code="404">User not found.</response>
    [HttpGet("{id}")]
    [Produces("application/json")]
    public ActionResult<UserDto> GetUserById(string id)
    {
        var user = mockDataService.GetUserById(id);
        if (user is null)
            return NotFound(new { message = $"User with ID '{id}' not found." });

        return Ok(MapToDto(user));
    }

    /// <summary>
    /// Gets all users (primarily for admin purposes).
    /// </summary>
    /// <returns>List of all users.</returns>
    /// <response code="200">Returns the list of users.</response>
    [HttpGet]
    [Produces("application/json")]
    public ActionResult<IEnumerable<UserDto>> GetAllUsers()
    {
        var users = mockDataService.GetAllUsers();
        var userDtos = users.Select(MapToDto);
        return Ok(userDtos);
    }

    /// <summary>
    /// Gets activities for a specific user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>List of user activities or 404 if user not found.</returns>
    /// <response code="200">Returns the user's activities.</response>
    /// <response code="404">User not found.</response>
    [HttpGet("{userId}/activities")]
    [Produces("application/json")]
    public ActionResult<IEnumerable<ActivityDto>> GetUserActivities(string userId)
    {
        // Verify user exists
        var user = mockDataService.GetUserById(userId);
        if (user is null)
            return NotFound(new { message = $"User with ID '{userId}' not found." });

        var activities = mockDataService.GetUserActivities(userId);
        var activityDtos = activities.Select(MapToActivityDto);
        return Ok(activityDtos);
    }

    /// <summary>
    /// Maps a User entity to a UserDto.
    /// </summary>
    private static UserDto MapToDto(BookHive.Core.Entities.User user) => new()
    {
        Id = user.Id,
        Username = user.Username,
        DisplayName = user.DisplayName,
        AvatarUrl = user.AvatarUrl,
        ProfileUrl = user.ProfileUrl,
        Location = user.Location
    };

    /// <summary>
    /// Maps an Activity entity to an ActivityDto.
    /// </summary>
    private static ActivityDto MapToActivityDto(BookHive.Core.Entities.Activity activity) => new()
    {
        Id = activity.Id,
        UserId = activity.UserId,
        ActivityType = activity.ActivityType,
        Rating = activity.Rating,
        ActivityContent = activity.ActivityContent,
        BookId = activity.BookId,
        Timestamp = activity.Timestamp,
        User = activity.User is not null ? MapToUserDto(activity.User) : null,
        Book = activity.Book is not null ? MapToBookDto(activity.Book) : null
    };

    private static UserDto MapToUserDto(BookHive.Core.Entities.User user) => new()
    {
        Id = user.Id,
        Username = user.Username,
        DisplayName = user.DisplayName,
        AvatarUrl = user.AvatarUrl,
        ProfileUrl = user.ProfileUrl,
        Location = user.Location
    };

    private static BookDto MapToBookDto(BookHive.Core.Entities.Book book) => new()
    {
        Id = book.Id,
        Title = book.Title,
        Description = book.Description,
        BookImageUrl = book.BookImageUrl,
        Author = book.Author,
        Publisher = book.Publisher,
        ListName = book.ListName,
        ListNameEncoded = book.ListNameEncoded
    };
}
