using BookHive.Core.DTOs;
using BookHive.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookHive.Api.Controllers;

/// <summary>
/// API controller for shelf-related operations.
/// Provides endpoints for retrieving shelves and user-specific shelf information.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ShelvesController(MockDataService mockDataService) : ControllerBase
{
    /// <summary>
    /// Gets all available shelves in the system.
    /// </summary>
    /// <returns>List of all shelves.</returns>
    /// <response code="200">Returns the list of shelves.</response>
    [HttpGet]
    [Produces("application/json")]
    public ActionResult<IEnumerable<ShelfDto>> GetAllShelves()
    {
        var shelves = mockDataService.GetAllShelves();
        var shelfDtos = shelves.Select(MapToDto);
        return Ok(shelfDtos);
    }

    /// <summary>
    /// Gets shelves for a specific user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>List of user's shelves or 404 if user not found.</returns>
    /// <response code="200">Returns the user's shelves.</response>
    /// <response code="404">User not found.</response>
    [HttpGet("user/{userId}")]
    [Produces("application/json")]
    public ActionResult<IEnumerable<ShelfDto>> GetUserShelves(string userId)
    {
        // Verify user exists
        var user = mockDataService.GetUserById(userId);
        if (user is null)
            return NotFound(new { message = $"User with ID '{userId}' not found." });

        var shelves = mockDataService.GetUserShelves(userId);
        var shelfDtos = shelves.Select(MapToDto);
        return Ok(shelfDtos);
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
