using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharingPlatform.Domain.Constants;
using SharingPlatform.Infrastructure.Core;

namespace SharingPlatform.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDbContextInMemory(this IServiceCollection services)
	{
		return services.AddDbContext<ApplicationDbContext>(
			options => options.UseInMemoryDatabase("InMemory"));
	}

	public static IServiceCollection AddDbContextSqlite(this IServiceCollection services)
	{
		return services.AddDbContext<ApplicationDbContext>(
			options => options.UseSqlite("Data Source=D:\\Projects\\sql\\SharingPlatform\\Database.db;"));
	}
}