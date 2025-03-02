using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SharingPlatform.Application.Settings;

namespace SharingPlatform.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDiscordSettings(this IServiceCollection services, IConfiguration configuration)
	{
		var discordSettings = configuration
			.GetSection("Discord")
			.Get<DiscordSettings>()!;

		services.AddSingleton(discordSettings);

		return services;
	}


	public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = services.AddJwtSettings(configuration);
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
            .AddJwtBearer(options =>
			 {
				 options.TokenValidationParameters = new TokenValidationParameters
				 {
					 ValidIssuer = jwtSettings.Issuer,
					 ValidAudience = jwtSettings.Audience,
					 ValidateAudience = false,
					 ValidateIssuer = false,
					 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
				 };
			 });
		services.AddAuthorization();

        return services;
    }
    
    private static JwtSettings AddJwtSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration
            .GetSection("JwtSettings")
            .Get<JwtSettings>()!;

        services.AddSingleton(jwtSettings);

        return jwtSettings;
    }
}