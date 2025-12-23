using BookHive.Core.DTOs;
using BookHive.Core.Providers;


namespace BookHive.Api.Services;

public sealed class ShelvesService(IShelfProvider _provider)
{
    public IEnumerable<ShelfDto> GetAllShelves()
    {
        var shelves = _provider.GetAllShelves();
        var shelfDtos = shelves.Select(MapToDto);
        return shelfDtos;
    }


    public IEnumerable<ShelfDto> GetUserShelves(string userId)
    {
        var shelves = _provider.GetUserShelves(userId);
        var shelfDtos = shelves.Select(MapToDto);
        return shelfDtos;
    }



    /// <summary>
    /// Maps a Shelf entity to a ShelfDto.
    /// </summary>
    private static ShelfDto MapToDto(BookHive.Core.Entities.Shelf shelf) => new()
    {
        Id = shelf.Id,
        Name = shelf.Name,
        Slug = shelf.Slug,
        Count = shelf.Count,
        IsCurated = shelf.IsCurated
    };

}
