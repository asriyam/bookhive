
using BookHive.Core.DTOs;
using BookHive.Infrastructure.Providers;
namespace BookHive.Api.Services;

public sealed class UsersService(UsersMockProvider _provider)
{
    public UserDto GetUserById(string userId)
    {
        var user = _provider.GetUserById(userId);
        var userDto = MapToUserDto(user);
        return userDto;
    }

    public IEnumerable<UserDto> GetAllUsers()
    {
        var users = _provider.GetAllUsers();
        var userDtos = users.Select(MapToDto);
        return userDtos;
    }

    public IEnumerable<ActivityDto> GetUserActivities(string userId)
    {
        var activities = _provider.GetUserActivities(userId);
        var activityDtos = activities.Select(MapToActivityDto);
        return activityDtos;
    }

    /// <summary>
    ///  Map user entity to DTO
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private static UserDto MapToUserDto(BookHive.Core.Entities.User user) => new()
    {
        Id = user.Id,
        Username = user.Username,
        DisplayName = user.DisplayName,
        AvatarUrl = user.AvatarUrl,
        ProfileUrl = user.ProfileUrl,
        Location = user.Location
    };

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

    /// <summary>
    /// Maps a Book entity to a BookDto.
    /// </summary>
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
