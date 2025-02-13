using System.Text.Json.Serialization;

namespace SharingPlatform.Application.Dtos;

internal sealed class ServerDataResponse
{
    [JsonPropertyName("expires_at")] public DateTime ExpiresAt { get; set; }
    [JsonPropertyName("guild")] public ServerDetails? Details { get; set; }

    public static bool IsValid(ServerDataResponse? response)
    {
        return response?.Details is not null;
    }
}

internal sealed record ServerDetails
{
    [JsonPropertyName("id")] public string Id { get; set; } = default!;
    [JsonPropertyName("name")] public string Name { get; set; } = default!;
    [JsonPropertyName("description")] public string Description { get; set; } = default!;
    [JsonPropertyName("icon")] public string IconHash { get; set; } = default!;
}