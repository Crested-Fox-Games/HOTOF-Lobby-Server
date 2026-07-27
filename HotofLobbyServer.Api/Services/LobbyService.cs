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

    public Lobby? JoinLobby(Guid id)
    {
        //Searches for lobby
        Lobby? lobby = lobbies.FirstOrDefault(x => x.Id == id);

        //Checks to see if it exists
        if (lobby == null)
            return null;

        //Checks to ensure not at max players
        if (lobby.CurrentPlayers >= lobby.MaxPlayers)
            return null;

        //Adds a player
        lobby.CurrentPlayers++;

        //Returns the lobby
        return lobby;
    }

    public bool LeaveLobby(Guid id)
    {
        Lobby? lobby = lobbies.FirstOrDefault(x => x.Id == id);

        if(lobby == null) 
            return false;

        lobby.CurrentPlayers--;

        //If no players left in lobby, remove lobby
        if(lobby.CurrentPlayers <= 0)
        {
            lobbies.Remove(lobby);
        }

        return true;
    }
}