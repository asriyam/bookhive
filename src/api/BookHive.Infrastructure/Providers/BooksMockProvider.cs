using BookHive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookHive.Infrastructure.Providers;

public sealed class BooksMockProvider
{
    private readonly List<Book> _books;

    public BooksMockProvider()
    {
        _books = InitializeBooks();
    }

    /// <summary>
    /// Gets all books from mock data.
    /// </summary>
    public IEnumerable<Book> GetAllBooks() => _books;


    /// <summary>
    /// Gets a book by ID.
    /// </summary>
    public Book? GetBookById(string id) => _books.FirstOrDefault(b => b.Id == id);


    /// <summary>
    /// Searches books by title, author, or description.
    /// </summary>
    public IEnumerable<Book> SearchBooks(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return _books;

        var lowerQuery = query.ToLower();
        return _books.Where(b =>
            b.Title.Contains(lowerQuery, StringComparison.OrdinalIgnoreCase) ||
            b.Author.Contains(lowerQuery, StringComparison.OrdinalIgnoreCase) ||
            b.Description.Contains(lowerQuery, StringComparison.OrdinalIgnoreCase)
        );
    }

    private List<Book> InitializeBooks()
    {
        return new List<Book>
        {
            new()
            {
                Id = "9780385548984",
                Title = "THE WIDOW",
                Description = "When Simon Latch, a lawyer in rural Virginia, is accused of murder, he goes in search of the real killer.",
                BookImageUrl = "https://static01.nyt.com/bestsellers/images/9780385548984.jpg",
                Author = "John Grisham",
                Publisher = "Doubleday",
                ListName = "Combined Print & E-Book Fiction",
                ListNameEncoded = "combined-print-and-e-book-fiction"
            },
            new()
            {
                Id = "9780385546898",
                Title = "THE SECRET OF SECRETS",
                Description = "As he searches for the missing noetic scientist he has been seeing, Robert Langdon discovers something regarding a secret project.",
                BookImageUrl = "https://static01.nyt.com/bestsellers/images/9780385546898.jpg",
                Author = "Dan Brown",
                Publisher = "Doubleday",
                ListName = "Combined Print & E-Book Fiction",
                ListNameEncoded = "combined-print-and-e-book-fiction"
            },
            new()
            {
                Id = "9781464257872",
                Title = "MURDER AT HOLLY HOUSE",
                Description = "When a dead stranger is found in a Yorkshire chimney around the holidays in 1952, Inspector Frank Grasby gets assigned the case.",
                BookImageUrl = "https://static01.nyt.com/bestsellers/images/9781464257872.jpg",
                Author = "Denzil Meyrick",
                Publisher = "Poisoned Pen",
                ListName = "Combined Print & E-Book Fiction",
                ListNameEncoded = "combined-print-and-e-book-fiction"
            },
            new()
            {
                Id = "9780593312032",
                Title = "CAMINO GHOSTS",
                Description = "A diverse cast of characters including a lawyer, a judge, and a group of locals navigate mysteries and secrets on a small Florida island.",
                BookImageUrl = "https://static01.nyt.com/bestsellers/images/9780593312032.jpg",
                Author = "John Grisham",
                Publisher = "Doubleday",
                ListName = "Combined Print & E-Book Fiction",
                ListNameEncoded = "combined-print-and-e-book-fiction"
            },
            new()
            {
                Id = "9780062457936",
                Title = "FOURTH WING",
                Description = "A young woman discovers a hidden world of dragons and must learn to survive in a dangerous academy.",
                BookImageUrl = "https://static01.nyt.com/bestsellers/images/9780062457936.jpg",
                Author = "Rebecca Yarros",
                Publisher = "Entangled Publishing",
                ListName = "Combined Print & E-Book Fiction",
                ListNameEncoded = "combined-print-and-e-book-fiction"
            }
        };
    }
}
