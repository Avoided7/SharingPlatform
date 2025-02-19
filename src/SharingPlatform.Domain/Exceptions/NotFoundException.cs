using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace SharingPlatform.Domain.Exceptions;

public class NotFoundException: BaseApplicationException
{
	private NotFoundException(string name) : base($"{name} not found.", (int)HttpStatusCode.NotFound) { }

	[DoesNotReturn]
	public static void Throw(Type type)
	{
		throw new NotFoundException(type.Name);
	}

	[DoesNotReturn]
	public static void Throw(string type)
	{
		throw new NotFoundException(type);
	}

	[DoesNotReturn]
	public static void ThrowFromModel(Type type)
	{
		var name = type.Name.Replace("Model", string.Empty);

		throw new NotFoundException(name);
	}
}