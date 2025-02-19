using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SharingPlatform.Domain.Exceptions;

public sealed class ValidationException : BaseApplicationException
{
	private ValidationException() : base("Validation failed.", (int)HttpStatusCode.BadRequest) { }

	[DoesNotReturn]
	public static void Throw()
	{
		throw new ValidationException();
	}
}