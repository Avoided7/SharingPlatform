using System.Text.Json.Serialization;
using SharingPlatform.Domain.Models;

namespace SharingPlatform.Application.Dtos;

public sealed class DiscordServerData
{
    [JsonPropertyName("expires_at")] public DateTime ExpiresAt { get; set; }
    [JsonPropertyName("guild")] public DiscordServerDetails Details { get; set; } = default!;
    [JsonPropertyName("approximate_member_count")] public int MembersTotal { get; set; }
    [JsonPropertyName("approximate_presence_count")] public int MembersOnline { get; set; }
    [JsonPropertyName("type")] public int InviteType { get; set; }

    public string PhotoUri => $"https://cdn.discordapp.com/icons/{Details!.GuildId}/{Details.IconHash}.png";


	public ServerModel ToModel(
	    string inviteLink,
	    string userId)
    {
	    var membersInfo = MembersInfoModel.Create(MembersOnline, MembersTotal);

	    var model = ServerModel.Create(
		    Details.Name,
		    Details.Description,
		    null,
		    PhotoUri,
		    inviteLink,
		    userId,
		    Details.GuildId,
		    membersInfo);

	    return model;
    }

    public static bool IsValid(DiscordServerData? response)
    {
        return response?.Details is not null && InviteTypes.IsGuild(response.InviteType);
    }
}

public sealed record DiscordServerDetails
{
    [JsonPropertyName("id")] public string GuildId { get; set; } = default!;
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