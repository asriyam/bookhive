using BookHive.Core.Entities;
using BookHive.Core.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookHive.Infrastructure.Providers;

public sealed class ShelvesMockProvider : IShelfProvider
{
    private readonly List<Shelf> _shelves;

    public ShelvesMockProvider()
    {
        _shelves = InitializeShelves();
    }

    /// <summary>
    /// Gets all shelves from mock data.
    /// </summary>
    public IEnumerable<Shelf>? GetAllShelves() => _shelves;

    /// <summary>
    /// Gets shelves for a specific user (returns all shelves for Phase 2).
    /// </summary>
    public IEnumerable<Shelf>? GetUserShelves(string userId) => _shelves;



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

}
