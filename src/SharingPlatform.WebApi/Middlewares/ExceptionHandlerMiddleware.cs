using System.Net;
using System.Text.Json;
using SharingPlatform.Domain.Exceptions;
using SharingPlatform.WebApi.Models.Responses;

namespace SharingPlatform.WebApi.Middlewares;

public sealed class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (BaseApplicationException exception)
		{
			await HandleExceptionAsync(context, exception, exception.StatusCode);
		}
		catch (Exception exception)
		{
			logger.LogWarning(exception, exception.Message);

			await HandleExceptionAsync(context, exception, (int)HttpStatusCode.InternalServerError);
		}
	}

	private static async Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode)
	{
		var response = ErrorResponse.FromException(exception);
		var json = JsonSerializer.Serialize(response);

		context.Response.StatusCode = statusCode;
		context.Response.ContentType = "application/json";

		await context.Response.WriteAsync(json);
	}
}