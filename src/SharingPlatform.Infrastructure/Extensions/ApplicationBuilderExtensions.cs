using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharingPlatform.Domain.Constants;
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

	public static async Task SeedDatabaseAsync(this IApplicationBuilder application)
	{
		await using var scope = application.ApplicationServices.CreateAsyncScope();

		var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
		if(!await roleManager.Roles.AnyAsync())
		{
			var roles = Roles.All;

			foreach(var role in roles)
			{
				await roleManager.CreateAsync(new IdentityRole(role));
			}
		}

		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
		if(!await userManager.Users.AnyAsync())
		{
			var user = new IdentityUser
			{
				Id = "c64ccf40-0c33-48e8-b55e-727f4f5233a4",
				UserName = "test",
				Email = "test@test.com"
			};

			await userManager.CreateAsync(user, "123");
			await userManager.AddToRoleAsync(user, Roles.Admin);
		}
	}
}