namespace SharingPlatform.Domain.Constants;

public static class Roles
{
    public const string Default = "Default";
    public const string Admin = "Admin";

    public static string[] All { get; } = typeof(Roles)
		   .GetFields()
		   .Select(field => (string)field.GetRawConstantValue()!)
		   .ToArray();
    
}