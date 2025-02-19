using System.Text.Json.Serialization;

namespace SharingPlatform.Application.Dtos;

internal sealed class ServerDataResponse
{
    [JsonPropertyName("expires_at")] public DateTime ExpiresAt { get; set; }
    [JsonPropertyName("guild")] public ServerDetails? Details { get; set; }
    [JsonPropertyName("approximate_member_count")] public int MembersTotal { get; set; }
    [JsonPropertyName("approximate_presence_count")] public int MembersOnline { get; set; }
    [JsonPropertyName("type")] public int InviteType { get; set; }

    public static bool IsValid(ServerDataResponse? response)
    {
        return response?.Details is not null && InviteTypes.IsGuild(response.InviteType);
    }
}

internal sealed record ServerDetails
{
    [JsonPropertyName("id")] public string Id { get; set; } = default!;
    [JsonPropertyName("name")] public string Name { get; set; } = default!;
    [JsonPropertyName("description")] public string Description { get; set; } = default!;
    [JsonPropertyName("icon")] public string IconHash { get; set; } = default!;
}

public static class InviteTypes
{
	public const int Guild = 0;
	public const int Group = 1;
	public const int Friend = 2;

    public static bool IsGuild(int type)
	{
		return type is Guild;
	}
}