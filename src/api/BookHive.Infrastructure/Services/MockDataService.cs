using BookHive.Core.Entities;

namespace BookHive.Infrastructure.Services;

/// <summary>
/// Service that provides mock data mirroring the Angular client's JSON data.
/// This service is used during Phase 2 development until database is integrated.
/// </summary>
public class MockDataService
{
    private readonly List<Book> _books;
    private readonly List<User> _users;
    private readonly List<Shelf> _shelves;
    private readonly List<Activity> _activities;

    public MockDataService()
    {
        _books = InitializeBooks();
        _users = InitializeUsers();
        _shelves = InitializeShelves();
        _activities = InitializeActivities();
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

    /// <summary>
    /// Gets all users from mock data.
    /// </summary>
    public IEnumerable<User> GetAllUsers() => _users;

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    public User? GetUserById(string id) => _users.FirstOrDefault(u => u.Id == id);

    /// <summary>
    /// Gets all shelves from mock data.
    /// </summary>
    public IEnumerable<Shelf> GetAllShelves() => _shelves;

    /// <summary>
    /// Gets shelves for a specific user (returns all shelves for Phase 2).
    /// </summary>
    public IEnumerable<Shelf> GetUserShelves(string userId) => _shelves;

    /// <summary>
    /// Gets all activities from mock data.
    /// </summary>
    public IEnumerable<Activity> GetAllActivities() => _activities;

    /// <summary>
    /// Gets activities for a specific user.
    /// </summary>
    public IEnumerable<Activity> GetUserActivities(string userId) =>
        _activities.Where(a => a.UserId == userId);

    #region Initialization Methods

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

    private List<User> InitializeUsers()
    {
        return new List<User>
        {
            new()
            {
                Id = "5813019",
                Username = "ElizaLu",
                DisplayName = "ElizaLu",
                AvatarUrl = "https://images.gr-assets.com/users/1483760406p6/5813019.jpg",
                ProfileUrl = "https://www.goodreads.com/user/show/5813019-elizalu",
                Location = "Chicago, IL"
            },
            new()
            {
                Id = "3906820",
                Username = "Vidya",
                DisplayName = "Vidya-BooksAreMagic",
                AvatarUrl = "https://images.gr-assets.com/users/1390203697p6/3906820.jpg",
                ProfileUrl = "https://www.goodreads.com/user/show/3906820-vidya-booksaremagic",
                Location = "Bengaluru, IN"
            },
            new()
            {
                Id = "279256",
                Username = "Diane",
                DisplayName = "Diane",
                AvatarUrl = "https://images.gr-assets.com/users/1497365060p6/279256.jpg",
                ProfileUrl = "https://www.goodreads.com/user/show/279256-diane",
                Location = "New York, NY"
            },
            new()
            {
                Id = "97027921",
                Username = "AMANDA",
                DisplayName = "AMANDA",
                AvatarUrl = "https://images.gr-assets.com/users/1739514128p6/97027921.jpg",
                ProfileUrl = "https://www.goodreads.com/user/show/97027921-amanda",
                Location = "Toronto, Canada"
            },
            new()
            {
                Id = "4271946",
                Username = "jessica-woodbury",
                DisplayName = "Jessica Woodbury",
                AvatarUrl = "https://images.gr-assets.com/users/1415105097p6/4271946.jpg",
                ProfileUrl = "https://www.goodreads.com/user/show/4271946-jessica-woodbury",
                Location = "Chicago, IL"
            }
        };
    }

    private List<Shelf> InitializeShelves()
    {
        return new List<Shelf>
        {
            new() { Id = 1, Name = "fantasy", Slug = "fantasy", Count = 19025085, IsCurated = true },
            new() { Id = 2, Name = "fiction", Slug = "fiction", Count = 18382588, IsCurated = true },
            new() { Id = 3, Name = "romance", Slug = "romance", Count = 13684190, IsCurated = true },
            new() { Id = 4, Name = "young adult", Slug = "young-adult", Count = 6531774, IsCurated = true },
            new() { Id = 5, Name = "mystery", Slug = "mystery", Count = 6381166, IsCurated = true },
            new() { Id = 6, Name = "classics", Slug = "classics", Count = 5722671, IsCurated = true },
            new() { Id = 7, Name = "audiobook", Slug = "audiobook", Count = 5092943, IsCurated = false },
            new() { Id = 8, Name = "horror", Slug = "horror", Count = 3533412, IsCurated = true },
            new() { Id = 9, Name = "thriller", Slug = "thriller", Count = 2742118, IsCurated = true },
            new() { Id = 10, Name = "coming-of-age", Slug = "coming-of-age", Count = 341822, IsCurated = false }
        };
    }

    private List<Activity> InitializeActivities()
    {
        return new List<Activity>
        {
            new()
            {
                Id = "act-1",
                UserId = "5813019",
                ActivityType = "rated",
                Rating = "5",
                ActivityContent = "ElizaLu rated The Widow 5 stars",
                BookId = "9780385548984",
                Timestamp = DateTime.UtcNow.AddDays(-2)
            },
            new()
            {
                Id = "act-2",
                UserId = "3906820",
                ActivityType = "read",
                Rating = "4",
                ActivityContent = "Vidya finished reading The Secret of Secrets",
                BookId = "9780385546898",
                Timestamp = DateTime.UtcNow.AddDays(-1)
            },
            new()
            {
                Id = "act-3",
                UserId = "279256",
                ActivityType = "rated",
                Rating = "4",
                ActivityContent = "Diane rated Murder at Holly House 4 stars",
                BookId = "9781464257872",
                Timestamp = DateTime.UtcNow
            },
            new()
            {
                Id = "act-4",
                UserId = "97027921",
                ActivityType = "read",
                Rating = null,
                ActivityContent = "AMANDA is currently reading Camino Ghosts",
                BookId = "9780593312032",
                Timestamp = DateTime.UtcNow.AddHours(-3)
            },
            new()
            {
                Id = "act-5",
                UserId = "4271946",
                ActivityType = "rated",
                Rating = "5",
                ActivityContent = "Jessica Woodbury rated Fourth Wing 5 stars",
                BookId = "9780062457936",
                Timestamp = DateTime.UtcNow.AddHours(-6)
            }
        };
    }

    #endregion
}
