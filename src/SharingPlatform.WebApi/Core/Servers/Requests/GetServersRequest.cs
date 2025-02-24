using Microsoft.AspNetCore.Mvc;
using SharingPlatform.WebApi.Models.Requests;

namespace SharingPlatform.WebApi.Core.Servers.Requests;

public sealed class GetServersRequest : PaginatedRequest
{
	[FromHeader(Name = "tags")] 
	public List<Guid>? TagsIds { get; set; }
}