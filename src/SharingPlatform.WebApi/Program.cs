using SharingPlatform.Application.Extensions;
using SharingPlatform.Infrastructure.Extensions;
using SharingPlatform.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddHttpClient();

builder.Services.AddApplicationServices();
builder.Services.AddDbContextInMemory();

builder.Services.AddApplicationIdentity();
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();