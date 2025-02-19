namespace SharingPlatform.Domain.Models;

public sealed class FavouriteModel
{
	public Guid Id { get; set; }
	public Guid ServerId { get; set; }
	public string UserId { get; set; } = default!;

	public ServerModel Server { get; set; } = default!;

	public static FavouriteModel Create(Guid serverId, string userId)
	{
		return new FavouriteModel
		{
			Id = Guid.NewGuid(),
			ServerId = serverId,
			UserId = userId
		};
	}
}