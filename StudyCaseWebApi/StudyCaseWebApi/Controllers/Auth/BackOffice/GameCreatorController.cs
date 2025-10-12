using System.Text.Json;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyCaseWebApi.Extensions;
using StudyCaseWebApi.Models;
using StudyCaseWebApi.Services;
using StudyCaseWebApi.Services.Entities;

namespace StudyCaseWebApi.Controllers.BackOffice;

[ApiController]
[Route("api/[controller]/[action]")]
public class GameCreatorController(IDataBaseService dataBaseService) : Controller
{
    [HttpPost]
    [Authorize(policy: "Admin")]
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