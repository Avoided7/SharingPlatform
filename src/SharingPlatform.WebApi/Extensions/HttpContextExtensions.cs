using System.Security.Claims;

namespace SharingPlatform.WebApi.Extensions;

public static class HttpContextExtensions
{
    public static string GetUserId(this HttpContext httpContext)
    {
        return httpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
    }
}