namespace Ecom.Api.Helper;

public class ApiExceptions : ResponseAPI
{
    public string Detials { get; set; }
    public ApiExceptions(int statusCode, string? message = null, string detials=null) : base(statusCode, message)
    {
        Detials = detials;
    }
}
 