using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyCaseWebApi.Extensions;
using StudyCaseWebApi.Models;
using StudyCaseWebApi.Services;

namespace StudyCaseWebApi.Controllers.BackOffice;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserCreatorController(IConfiguration configuration, IDataBaseService dataBaseService) : Controller
{
    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> CreateUser([FromBody] LoginResponse loginResponse)
    {
        return Ok(await loginResponse.CreateAccount(configuration, dataBaseService));
    }
}