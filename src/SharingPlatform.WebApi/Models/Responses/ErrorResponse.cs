namespace SharingPlatform.WebApi.Models.Responses;

public sealed record ErrorResponse(string Message)
{
	public static ErrorResponse FromException(Exception exception) => new ErrorResponse(exception.Message);
}