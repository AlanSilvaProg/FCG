using System.Text.Json;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyCaseWebApi.Extensions;
using StudyCaseWebApi.Models;
using StudyCaseWebApi.Services;
using StudyCaseWebApi.Services.Entities;

namespace StudyCaseWebApi.Controllers.BackOffice;

/// <summary>
/// Controller for managing game registrations
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class GameCreatorController(IDataBaseService dataBaseService) : Controller
{
    /// <summary>
    /// Registers a new game in the system
    /// </summary>
    /// <param name="gameRegistry">The game information to register</param>
    /// <returns>Response indicating success or failure</returns>
    /// <response code="200">Returns success response if game registration is successful</response>
    /// <response code="500">If there's an error during registration or if required fields are missing</response>
    [HttpPost]
    [Authorize(policy: "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Response>> RegisterNewGame([FromBody] GameRegistry gameRegistry)
    {
        if (gameRegistry.id == Guid.Empty || gameRegistry.name == string.Empty)
        {
            return StatusCode(500, new
            {
                message = $"ID and Game name are required to create a new Registry"
            });
        }

        var existentGame = dataBaseService.GameRegistry().AsNoTracking()
            .FirstOrDefault(g => g.name == gameRegistry.name);

        if (existentGame != null)
        {
            return Ok(new Response(false, JsonSerializer.Serialize(existentGame), "Game already exists"));
        }
        
        gameRegistry = gameRegistry with { id = Guid.NewGuid(), registry_date = DateTime.UtcNow };

        try
        {
            await gameRegistry.SaveOnDatabase(dataBaseService);
        }
        catch (Exception e)
        {
            return StatusCode(500, new
            {
                message = "Internal server error while registering",
                detail = e.InnerException?.Message ?? e.Message
            });
        }
        
        return Ok(new Response(true, JsonSerializer.Serialize(gameRegistry), gameRegistry.name));
    }
}