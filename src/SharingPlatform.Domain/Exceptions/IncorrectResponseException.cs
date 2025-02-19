using System.Diagnostics.CodeAnalysis;

namespace SharingPlatform.Domain.Exceptions;

public sealed class IncorrectResponseException : BaseApplicationException
{
	private IncorrectResponseException() : base("Incorrect response.") { }

	[DoesNotReturn]
	public static void Throw()
	{
		throw new IncorrectResponseException();
	}
}