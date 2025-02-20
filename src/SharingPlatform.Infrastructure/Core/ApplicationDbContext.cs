using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharingPlatform.Domain.Models;

namespace SharingPlatform.Infrastructure.Core;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<ServerModel> Servers { get; set; }
    public DbSet<TagModel> Tags { get; set; }
    public DbSet<FavouriteModel> Favourites { get; set; }
    public DbSet<RatingModel> Ratings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
	    builder
		    .Entity<ServerModel>()
		    .Navigation(server => server.Tags)
		    .AutoInclude();

	    builder
		    .Entity<ServerModel>()
		    .Navigation(server => server.MembersInfo)
		    .AutoInclude();

		builder.Entity<ServerModel>()
		    .Navigation(server => server.Ratings)
		    .AutoInclude();

		base.OnModelCreating(builder);
    }
}