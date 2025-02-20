using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SharingPlatform.Domain.Exceptions;

public sealed class AlreadyExistException(string name)
	: BaseApplicationException($"{name} already exist.", (int)HttpStatusCode.Conflict)
{
	[DoesNotReturn]
	public static void Throw(string name)
	{
		throw new AlreadyExistException(name);
	}

	[DoesNotReturn]
	public static void ThrowFromModel(string name)
	{
		name = name.Replace("Model", string.Empty);

		throw new AlreadyExistException(name);
	}
}