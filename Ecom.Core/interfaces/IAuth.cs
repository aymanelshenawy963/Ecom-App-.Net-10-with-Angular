using Ecom.Core.DTO;

namespace Ecom.Core.interfaces;

public interface IAuth
{
    Task<string> RegisterAsync(RegisterDTO registerDTO);
    Task<string> LoginAsync(LoginDTO loginDTO);
    Task<bool> SendEmailForForgetPassword(string email);
    Task<string> ResetPassword(ResetPasswordDTO resetPassword);
    Task<bool> ActiveEmail(ActiveAccountDTO activeAccount);

}
