namespace SharingPlatform.Domain.Exceptions;

public abstract class BaseApplicationException(string message, int statusCode) : Exception(message)
{
	protected BaseApplicationException(string message) : this(message, 500) { }

	public int StatusCode { get; } = statusCode;
}