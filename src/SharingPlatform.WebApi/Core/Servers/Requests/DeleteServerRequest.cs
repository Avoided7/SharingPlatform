using SharingPlatform.Domain.Models;

namespace SharingPlatform.WebApi.Core.Servers.Requests;

public sealed record DeleteServerRequest(Guid ServerId)
{
	public ServerModel ToModel(string userId)
	{
		var server = ServerModel.Empty;

		server.Id = ServerId;
		server.UserId = userId;

		return server;
	}
}