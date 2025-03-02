using Microsoft.Extensions.DependencyInjection;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.Application.Factories;
using SharingPlatform.Application.Services;

namespace SharingPlatform.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IServersService, ServersService>();
        services.AddScoped<ITagsService, TagsService>();
        services.AddScoped<IFavouritesService, FavouritesService>();
        services.AddScoped<IRatingsService, RatingsService>();
        services.AddScoped<IDiscordService, DiscordService>();

        services.AddScoped<ITokenFactory, TokenFactory>();

        services.AddHttpClient<IDiscordService, DiscordService>(client => client.BaseAddress = new Uri("https://discord.com/api/"));

		return services;
    }
}