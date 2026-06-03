using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestTask.Infrastructure.Contact.Model;
using TestTask.Web.Controllers.Bases;
using TestTask.Web.Models;

namespace TestTask.Web.Controllers.Api;

public sealed class AuthController(SignInManager<User> signInManager) : CandidatesAppControllerBase
{
    [AllowAnonymous]
    public IActionResult GetSession()
    {
        var result = new SessionResponse
        {
            IsAuthenticated = User.Identity is { IsAuthenticated: true },
            Login = User.Identity!.Name
        };

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRrequest request)
    {
        var signInResult = await signInManager.PasswordSignInAsync(request.Login, request.Password, false, false);

        if (!signInResult.Succeeded)
            return Unauthorized(new ProblemDetails { Detail = "Неверный логин или пароль." });

        var result = new SessionResponse
        {
            IsAuthenticated = true,
            Login = request.Login
        };

        return Ok(result);
    }

    [HttpDelete("logout")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();

        return NoContent();
    }
}
