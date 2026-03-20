using Ecom.Core.DTO;
using Ecom.Core.Entites;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.Core.Sharing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;


namespace Ecom.infrastructure.Repositriers;

public class AuthRepository : IAuth
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IGenerateToken _generateToken;

    public AuthRepository(UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, IGenerateToken generateToken)
    {
        _userManager = userManager;
        _emailService = emailService;
        _signInManager = signInManager;
        _generateToken = generateToken;
    }

    public async Task<string> RegisterAsync(RegisterDTO registerDTO)
    {
        if (registerDTO is null)
            return null!;

        if (await _userManager.FindByNameAsync(registerDTO.UserName) is not null)
            return "this UserName is already registerd";

        if (await _userManager.FindByEmailAsync(registerDTO.Email) is not null)
            return "this Email is already registered";

        AppUser user = new AppUser
        {
            Email = registerDTO.Email,
            UserName = registerDTO.UserName,
            DisplayName = registerDTO.DisplayName
        };

        var result = await _userManager.CreateAsync(user,registerDTO.Password);

        if (result.Succeeded is not true)
            return result.Errors.ToList()[0].Description;

        //Send Active Email
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        await SendEmail(user.Email!, encodedToken, "active", "ActiveEmail", "Please activate your email");

        return "done";

    }

    public async Task SendEmail(string email,string code,string component, string subject,string message)
    {
        var result = new EmailDTO(email,
            "aymoom473@gmail.com",
            subject,
            EmailStringBody.send(email,code,component,message));

        await _emailService.SendEmail(result);
    }

    public async Task<string> LoginAsync(LoginDTO login)
    {
        if (login is null)
            return null!;
    
        var user = await _userManager.FindByEmailAsync(login.Email);

        if (user is null)
            return "Please check email or password one of them is incorrect";

        if (!user.EmailConfirmed)
            {
               string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
               var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token)); 

               await SendEmail(user.Email!, encodedToken, "active", "ActiveEmail",
                   "Please active your email, click on button to active");

               return "Please active your email, we send you an email to active it";
            }
    
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, true);
    
            if (result.Succeeded )
                return _generateToken.GetAndCreateToken(user);

        return "Please check email or password one of them is incorrect";
    }

    public async Task<bool> SendEmailForForgetPassword(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return false;
        string token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        await SendEmail(user.Email!, encodedToken, "reset-password", "Reset Password", "Please click on button to reset your password");
        return true;
    }
     public async Task<string> ResetPassword(ResetPasswordDTO resetPassword)
    {
        var user = await _userManager.FindByEmailAsync(resetPassword.Email);
        if (user is null)
            return "this email not found";

        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPassword.Token));
        var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetPassword.Password);
        if (result.Succeeded)
            return "Password changed success";

        return result.Errors.First().Description;
    }

    public async Task<bool> ActiveEmail(ActiveAccountDTO activeAccount)
    {
        var user = await _userManager.FindByEmailAsync(activeAccount.Email);
        if (user is null)
            return false;

        if (user.EmailConfirmed)
            return true;

        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(activeAccount.Token));

        var result = await _userManager.ConfirmEmailAsync(user, decodedToken); 
        if (result.Succeeded)
            return true;
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        await SendEmail(user.Email!, encodedToken, "active", "ActiveEmail", "Please activate your email");
        return false;
    }
}
