namespace Ecom.Api.Helper;

public class ResponseAPI
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public ResponseAPI(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetMessageFromStatusCode(statusCode);
    }

    public string GetMessageFromStatusCode(int statusCode)
    {
        return StatusCode switch
        {
            200 => "Done",
            201 => "Created",
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            500 => "Internal Server Error",
            _ => "Unknown Status Code"
        };
    }
}
