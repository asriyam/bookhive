using BookHive.Core.Entities;

namespace BookHive.Core.Providers;

/// <summary>
/// Interface for book data provider.
/// Abstracts the data source (mock, database, API, etc.)
/// </summary>
public interface IBookProvider
{
    /// <summary>
    /// Gets all books.
    /// </summary>
    IEnumerable<Book>? GetAllBooks();

    /// <summary>
    /// Gets a book by ID (ISBN).
    /// </summary>
    Book? GetBookById(string id);

    /// <summary>
    /// Searches books by title, author, or description.
    /// </summary>
    IEnumerable<Book>? SearchBooks(string query);
}
