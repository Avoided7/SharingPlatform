using Microsoft.AspNetCore.Mvc;

namespace SharingPlatform.WebApi.Models.Requests;

public class PaginatedRequest
{
	[FromQuery] 
	public int Page { get; set; } = 1;

	[FromQuery] 
	public int PageSize { get; set; } = 10;

	public virtual bool IsValid()
	{
		return Page >= 1 && PageSize > 0;
	}
};