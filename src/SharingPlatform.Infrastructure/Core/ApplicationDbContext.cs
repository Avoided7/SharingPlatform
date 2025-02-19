using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharingPlatform.Domain.Models;

namespace SharingPlatform.Infrastructure.Core;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<ServerModel> Servers { get; set; }
    public DbSet<TagModel> Tags { get; set; }
    public DbSet<FavouriteModel> Favourites { get; set; }
}