using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SharingPlatform.Domain.Models;

public sealed class RatingModel
{
	public Guid Id { get; set; }
	public double Value { get; set; }
	public string? Comment { get; set; }

	public string UserId { get; set; } = default!;
	[ForeignKey(nameof(UserId))]
	public IdentityUser User { get; set; } = default!;


	public Guid ServerId { get; set; }
	[ForeignKey(nameof(ServerId))]
	public ServerModel Server { get; set; } = default!;

	public static RatingModel Create(double value, string? comment, string userId, Guid serverId)
	{
		return new RatingModel
		{
			Id = Guid.NewGuid(),
			Value = value,
			Comment = comment,
			UserId = userId,
			ServerId = serverId
		};
	}

	public void Update(RatingModel rating)
	{
		Value = rating.Value;
	}
}