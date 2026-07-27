using HotofLobbyServer.Api.Models;

namespace HotofLobbyServer.Api.Services;

public class LobbyService
{
    private readonly List<Lobby> lobbies = new();

    public List<Lobby> GetLobbies()
    {
        return lobbies;
    }

    public Lobby? GetLobby(Guid id)
    {
        return lobbies.FirstOrDefault(x => x.Id == id);
    }

    public Lobby CreateLobby(CreateLobbyRequest request)
    {
        Lobby lobby = new()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            HostIp = request.HostIp,
            MaxPlayers = request.MaxPlayers,
            InGame = false,
            lastHeartbeat = DateTime.UtcNow
        };

        lobby.Players.Add(new LobbyPlayer
        {
            Id = request.PlayerId,
            Name = request.PlayerName,
            isHost = true,
        });

        lobbies.Add(lobby);

        return lobby;
    }

    public Lobby? JoinLobby(Guid id, JoinLobbyRequest request)
    {
        //Searches for lobby
        Lobby? lobby = lobbies.FirstOrDefault(x => x.Id == id);

        //Checks to see if it exists
        if (lobby == null)
            return null;

        //Checks to ensure not at max players
        if (lobby.CurrentPlayers >= lobby.MaxPlayers)
            return null;

        lobby.Players.Add(new LobbyPlayer
        {
            Id = request.PlayerId,
            Name = request.PlayerName,
            isHost = false
        });

        //Returns the lobby
        return lobby;
    }

    public bool LeaveLobby(Guid id)
    {
        Lobby? lobby = lobbies.FirstOrDefault(x => x.Id == id);

        if(lobby == null) 
            return false;

        //If no players left in lobby, remove lobby
        if(lobby.CurrentPlayers <= 0)
        {
            lobbies.Remove(lobby);
        }

        //TODO: Remove player from the list

        return true;
    }
}