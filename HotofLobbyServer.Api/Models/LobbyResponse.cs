
using HotofLobbyServer.Api.Models;

public class LobbyResponse
{
    public required Lobby Lobby {  get; set; }

    public Guid PlayerId { get; set; }
        
}