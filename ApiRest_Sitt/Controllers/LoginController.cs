using Microsoft.AspNetCore.Mvc;
using ApiRest_Sitt.Models.Login;
using ApiRest_Sitt.Services.Login;
using ApiRest_Sitt.Services.Tasks;
using System.Security.Claims;

namespace ApiRest_Sitt.Controllers;


[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginServices _LoginServices;
    
    public LoginController(ILoginServices loginServices)
    {
        _LoginServices = loginServices;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> login([FromBody] LoginRequest autorization)
    {

        var result = await _LoginServices.ReturnToken(autorization);
        if (result == null)
        {
            return Unauthorized();
        }

        return Ok(result);

    }

    // POST: api/Tasks
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> PostTask([FromBody] LoginRequest autorization)
    {

        var register = await _LoginServices.Register(autorization);
    
        return Ok(register);
    }
}
