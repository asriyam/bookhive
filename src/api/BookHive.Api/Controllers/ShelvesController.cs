using BookHive.Api.Services;
using BookHive.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookHive.Api.Controllers;

/// <summary>
/// API controller for shelf-related operations.
/// Provides endpoints for retrieving shelves and user-specific shelf information.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class ShelvesController(ShelvesService _shelvesService, UsersService _userService) : ControllerBase
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
        var shelves = _shelvesService.GetAllShelves();
        return Ok(shelves);
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
        var user = _userService.GetUserById(userId);
        if (user is null)
            return NotFound(new { message = $"User with ID '{userId}' not found." });

        var shelves = _shelvesService.GetUserShelves(userId);
        return Ok(shelves);
    }

  
}
