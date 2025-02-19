using Microsoft.AspNetCore.Mvc;

namespace SharingPlatform.WebApi.Models.Requests;

public sealed record PaginatedRequest(
	[FromQuery] int Page = 1,
	[FromQuery] int PageSize = 10)
{
	public bool IsValid()
	{
		return Page >= 1 && PageSize > 0;
	}
};