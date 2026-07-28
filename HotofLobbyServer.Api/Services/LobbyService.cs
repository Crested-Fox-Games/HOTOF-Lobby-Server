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

    public LobbyResponse CreateLobby(CreateLobbyRequest request)
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

        LobbyPlayer player = new LobbyPlayer
        {
            Id = Guid.NewGuid(),
            Name = request.PlayerName,
            isHost = true
        };

        lobbies.Add(lobby);

        lobby.Players.Add(player);

        //Returns the lobby
        return new LobbyResponse
        {
            Lobby = lobby,
            PlayerId = player.Id
        };
    }

    public LobbyResponse? JoinLobby(Guid id, JoinLobbyRequest request)
    {
        //Searches for lobby
        Lobby? lobby = lobbies.FirstOrDefault(x => x.Id == id);

        //Checks to see if it exists
        if (lobby == null)
            return null;

        //Checks to ensure not at max players
        if (lobby.CurrentPlayers >= lobby.MaxPlayers)
            return null;

        LobbyPlayer player = new LobbyPlayer
        {
            Id = Guid.NewGuid(),
            Name = request.PlayerName,
            isHost = false
        };

        lobby.Players.Add(player);

        //Returns the lobby
        return new LobbyResponse
        {
            Lobby = lobby,
            PlayerId = player.Id
        };
    }

    public Lobby? LeaveLobby(Guid lobbyId, LeaveLobbyRequest request)
    {
        Lobby? lobby = GetLobby(lobbyId);

        if(lobby == null)
            return null;

        LobbyPlayer? player = lobby.Players.FirstOrDefault(player => player.Id == request.PlayerId);
        
        if(player == null)
            return null;

        if(player.isHost)
        {
            lobbies.Remove(lobby);

            return null;
        }

        lobby.Players.Remove(player);

        return lobby;
    }
}