using System.ComponentModel.DataAnnotations.Schema;

namespace SharingPlatform.Domain.Models;

public sealed class RatingModel
{
	public Guid Id { get; set; }
	public double Value { get; set; }
	public string UserId { get; set; } = default!;

	[ForeignKey(nameof(Server))]
	public Guid ServerId { get; set; }
	public ServerModel Server { get; set; } = default!;

	public static RatingModel Create(double value, string userId, Guid serverId)
	{
		return new RatingModel
		{
			Id = Guid.NewGuid(),
			Value = value,
			UserId = userId,
			ServerId = serverId
		};
	}

	public void Update(RatingModel rating)
	{
		Value = rating.Value;
	}
}