using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharingPlatform.Infrastructure.Core;

namespace SharingPlatform.Infrastructure.Extensions;

public static class ApplicationBuilderExtensions
{
	public static async Task ApplyMigrationsAsync(this IApplicationBuilder application)
	{
		await using var scope = application.ApplicationServices.CreateAsyncScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

		await dbContext.Database.MigrateAsync();
	}

	public static async Task ClearDatabaseAsync(this IApplicationBuilder application)
	{
		await using var scope = application.ApplicationServices.CreateAsyncScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

		await dbContext.Database.EnsureDeletedAsync();
	}
}