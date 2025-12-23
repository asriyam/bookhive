using BookHive.Core.Entities;

namespace BookHive.Core.Providers;

/// <summary>
/// Interface for user and activity data provider.
/// Abstracts the data source (mock, database, API, etc.)
/// </summary>
public interface IUserProvider
{
    /// <summary>
    /// Gets all users.
    /// </summary>
    IEnumerable<User> GetAllUsers();

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    User? GetUserById(string id);

    /// <summary>
    /// Gets all activities.
    /// </summary>
    IEnumerable<Activity> GetAllActivities();

    /// <summary>
    /// Gets activities for a specific user.
    /// </summary>
    IEnumerable<Activity> GetUserActivities(string userId);
}
