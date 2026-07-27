namespace HotofLobbyServer.Api.Models;

public class Lobby
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string HostIp { get; set; } = string.Empty;

    public List<LobbyPlayer> Players { get; set; } = new();

    public int CurrentPlayers => Players.Count;

    public int MaxPlayers { get; set; }

    public bool InGame { get; set; }

    public DateTime lastHeartbeat { get; set; }
}