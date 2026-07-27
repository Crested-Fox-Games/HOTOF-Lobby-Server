using HotofLobbyServer.Api.Models;

namespace HotofLobbyServer.Api.Services;

public class LobbyService
{
    private readonly List<Lobby> lobbies = new();

    public List<Lobby> GetLobbies()
    {
        return lobbies;
    }

    public Lobby CreateLobby(string name, string hostIP, int maxPlayers)
    {
        Lobby lobby = new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            HostIp = hostIP,
            CurrentPlayers = 1,
            MaxPlayers = maxPlayers,
            InGame = false,
            lastHeartbeat = DateTime.UtcNow
        };

        lobbies.Add(lobby);

        return lobby;
    }
}