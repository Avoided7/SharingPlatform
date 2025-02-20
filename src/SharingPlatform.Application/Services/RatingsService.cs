using Microsoft.EntityFrameworkCore;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.Domain.Exceptions;
using SharingPlatform.Domain.Models;
using SharingPlatform.Infrastructure.Core;

namespace SharingPlatform.Application.Services;

internal sealed class RatingsService(ApplicationDbContext dbContext) : IRatingsService
{
	public async Task RateAsync(RatingModel rating)
	{
		var serverExists = await dbContext.Servers.AnyAsync(server => server.Id == rating.ServerId);
		if (!serverExists)
		{
			NotFoundException.ThrowFromModel(typeof(ServerModel));
		}
		
		var userExists = await dbContext.Users.AnyAsync(user => user.Id == rating.UserId);
		if (!userExists)
		{
			NotFoundException.ThrowFromModel(typeof(UserModel));
		}

		var exist = await dbContext.Ratings.FirstOrDefaultAsync(exist => exist.ServerId == rating.ServerId && exist.UserId == rating.UserId);

		if (exist is null)
		{
			dbContext.Ratings.Add(rating);
		}
		else
		{
			exist.Update(rating);
			dbContext.Ratings.Update(exist);
		}

		await dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(RatingModel rating)
	{
		var exist = await dbContext.Ratings.FirstOrDefaultAsync(exist => exist.ServerId == rating.ServerId && exist.UserId == rating.UserId);

		if (exist is null)
		{
			NotFoundException.ThrowFromModel(typeof(RatingModel));
		}

		dbContext.Ratings.Remove(exist);
		await dbContext.SaveChangesAsync();
	}
}