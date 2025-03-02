using SharingPlatform.Application.Extensions;
using SharingPlatform.Infrastructure.Extensions;
using SharingPlatform.WebApi.Extensions;
using SharingPlatform.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSingleton<ExceptionHandlerMiddleware>();

builder.Services.AddApplicationServices();
builder.Services.AddDbContextSqlite();

builder.Services.AddDiscordSettings(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseCors(config =>
        config
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
}

await app.ClearDatabaseAsync();
await app.ApplyMigrationsAsync();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();