using BookHive.Core.Entities;

namespace BookHive.Core.Providers;

/// <summary>
/// Interface for shelf data provider.
/// Abstracts the data source (mock, database, API, etc.)
/// </summary>
public interface IShelfProvider
{
    /// <summary>
    /// Gets all available shelves.
    /// </summary>
    IEnumerable<Shelf>? GetAllShelves();

    /// <summary>
    /// Gets shelves for a specific user.
    /// </summary>
    IEnumerable<Shelf>? GetUserShelves(string userId);
}
