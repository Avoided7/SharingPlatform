using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharingPlatform.Infrastructure.Core;

namespace SharingPlatform.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbContextInMemory(this IServiceCollection services)
    {
        return services.AddDbContext<ApplicationDbContext>(
            options => options.UseInMemoryDatabase("InMemory"));
    }
}