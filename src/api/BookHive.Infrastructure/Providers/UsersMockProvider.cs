using BookHive.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookHive.Infrastructure.Providers;

public sealed class UsersMockProvider
{

    private readonly List<Activity> _activities;
    private readonly List<User> _users;


    public UsersMockProvider()
    {
        _activities = InitializeActivities();
        _users = InitializeUsers();

    }

    /// <summary>
    /// Gets all activities from mock data.
    /// </summary>
    public IEnumerable<Activity> GetAllActivities() => _activities;

    /// <summary>
    /// Gets activities for a specific user.
    /// </summary>
    public IEnumerable<Activity> GetUserActivities(string userId) =>
        _activities.Where(a => a.UserId == userId);


    /// <summary>
    /// Gets all users from mock data.
    /// </summary>
    public IEnumerable<User> GetAllUsers() => _users;

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    public User? GetUserById(string id) => _users.FirstOrDefault(u => u.Id == id);

    #region Initialization Methods
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

