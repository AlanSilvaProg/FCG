using Microsoft.AspNetCore.Mvc;
using StudyCaseWebApi.Extensions;
using StudyCaseWebApi.Models;
using StudyCaseWebApi.Services;

namespace StudyCaseWebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthenticationController(IConfiguration configuration, IDataBaseService dataBaseService) : Controller
{
    [HttpPost]
    public async Task<ActionResult<string>> Login([FromBody] LoginResponse loginResponse)
    {
        var loginResult = await loginResponse.Login(configuration, dataBaseService);
        
        return Ok(new { loginResult });
    }

    [HttpPost]
    public ActionResult<Response> CreateAccount([FromBody] LoginResponse loginResponse)
    {
        return Ok(loginResponse.CreateAccount(configuration, dataBaseService));
    }
}