namespace SharingPlatform.Domain.Exceptions;

public class NotFoundException(string name) : Exception($"{name} not found.");