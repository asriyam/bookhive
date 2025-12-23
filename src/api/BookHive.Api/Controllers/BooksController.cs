using BookHive.Api.Services;
using BookHive.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookHive.Api.Controllers;

/// <summary>
/// API controller for book-related operations.
/// Provides endpoints for retrieving books, searching, and accessing book details.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class BooksController(BooksService mockDataService) : ControllerBase
{
    /// <summary>
    /// Gets all books from the catalog.
    /// </summary>
    /// <returns>List of all books.</returns>
    /// <response code="200">Returns the list of books.</response>
    [HttpGet]
    [Produces("application/json")]
    public ActionResult<IEnumerable<BookDto>> GetBooks()
    {
        var books = mockDataService.GetBooks();
        return Ok(books);
    }

    /// <summary>
    /// Gets a specific book by ID.
    /// </summary>
    /// <param name="id">The book ID (ISBN).</param>
    /// <returns>The book details or 404 if not found.</returns>
    /// <response code="200">Returns the book.</response>
    /// <response code="404">Book not found.</response>
    [HttpGet("{id}")]
    [Produces("application/json")]
    public ActionResult<BookDto> GetBookById(string id)
    {
        var book = mockDataService.GetBookById(id);
        if (book is null)
            return NotFound(new { message = $"Book with ID '{id}' not found." });

        return Ok(book);
    }

    /// <summary>
    /// Searches books by query string.
    /// Searches in title, author, and description fields.
    /// </summary>
    /// <param name="query">The search query string.</param>
    /// <returns>List of books matching the search query.</returns>
    /// <response code="200">Returns the matching books.</response>
    [HttpGet("search")]
    [Produces("application/json")]
    public ActionResult<IEnumerable<BookDto>> SearchBooks([FromQuery] string? query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Ok(Enumerable.Empty<BookDto>());

        var results = mockDataService.SearchBooks(query);
        return Ok(results);
    }    
}
