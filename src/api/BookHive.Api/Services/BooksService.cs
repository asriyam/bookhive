using BookHive.Core.DTOs;
using BookHive.Infrastructure.Providers;

namespace BookHive.Api.Services;

public sealed class BooksService(BooksMockProvider _provider)
{
    public IEnumerable<BookDto> GetBooks()
    {
        var books = _provider.GetAllBooks();
        var bookDtos = books.Select(MapToDto);
        return bookDtos;
    }

    public BookDto? GetBookById(string id)
    {
        var book = _provider.GetBookById(id);
        return MapToDto(book);
    }

    public IEnumerable<BookDto> SearchBooks(string query)
    {
        var results = _provider.SearchBooks(query);
        var bookDtos = results.Select(MapToDto);
        return bookDtos;
    }


    private static BookDto MapToDto(BookHive.Core.Entities.Book book) => new()
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
