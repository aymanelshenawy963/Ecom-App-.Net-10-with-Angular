using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.DTO;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers;

public class AccountController : BaseController
{
    public AccountController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO registerDTO)
    {
        string result = await work.Auth.RegisterAsync(registerDTO);
        if (result != "done")
            return BadRequest(new ResponseAPI(400, result));

        return Ok(new ResponseAPI(200, result));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        string result = await work.Auth.LoginAsync(loginDTO);
        if(result.StartsWith("Please"))
            return BadRequest(new ResponseAPI(400, result));

        Response.Cookies.Append("token", result, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Domain = "localhost",
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(1)
        });

        return Ok(new ResponseAPI(200, "done"));
    }

    [HttpPost("active-account")]
    public async Task<IActionResult> ActiveAccount(ActiveAccountDTO activeAccountDTO)
    {
        var result = await work.Auth.ActiveEmail(activeAccountDTO);
        return result ? Ok(new ResponseAPI(200, "Account activated successfully")) :
            BadRequest(new ResponseAPI(400, "Invalid token or email"));
    }

    [HttpGet("send-email-forget-password")]
    public async Task<IActionResult> ForgetPassword(string email)
    {
        var result = await work.Auth.SendEmailForForgetPassword(email);
        return result ? Ok(new ResponseAPI(200, "Email sent successfully")) :
            BadRequest(new ResponseAPI(400, "Failed to send email"));
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
    {
        var result = await work.Auth.ResetPassword(resetPasswordDTO);

        if (result.StartsWith("this"))
            return BadRequest(new ResponseAPI(400, result));

        return Ok(new ResponseAPI(200, "Password reset successfully"));
    }
}