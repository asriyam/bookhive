using BookHive.Api.Services;
using BookHive.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookHive.Api.Controllers;

/// <summary>
/// API controller for user-related operations.
/// Provides endpoints for retrieving user information and user-specific data.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class UsersController(UsersService mockDataService) : ControllerBase
{
    /// <summary>
    /// Gets a specific user by ID.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <returns>The user details or 404 if not found.</returns>
    /// <response code="200">Returns the user.</response>
    /// <response code="404">User not found.</response>
    [HttpGet("{id}")]
    [Produces("application/json")]
    public ActionResult<UserDto> GetUserById(string id)
    {
        var user = mockDataService.GetUserById(id);
        if (user is null)
            return NotFound(new { message = $"User with ID '{id}' not found." });

        return Ok(user);
    }

    /// <summary>
    /// Gets all users (primarily for admin purposes).
    /// </summary>
    /// <returns>List of all users.</returns>
    /// <response code="200">Returns the list of users.</response>
    [HttpGet]
    [Produces("application/json")]
    public ActionResult<IEnumerable<UserDto>> GetAllUsers()
    {
        var userDtos = mockDataService.GetAllUsers();
        return Ok(userDtos);
    }

    /// <summary>
    /// Gets activities for a specific user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>List of user activities or 404 if user not found.</returns>
    /// <response code="200">Returns the user's activities.</response>
    /// <response code="404">User not found.</response>
    [HttpGet("{userId}/activities")]
    [Produces("application/json")]
    public ActionResult<IEnumerable<ActivityDto>> GetUserActivities(string userId)
    {
        // Verify user exists
        var user = mockDataService.GetUserById(userId);
        if (user is null)
            return NotFound(new { message = $"User with ID '{userId}' not found." });

        var activities = mockDataService.GetUserActivities(userId);
        return Ok(activities);
    }  
}
