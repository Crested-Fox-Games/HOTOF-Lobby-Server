namespace HotofLobbyServer.Api.Models;
public class CreateLobbyRequest
{
    public string Name { get; set; } = string.Empty;

    public string HostIp {  get; set; } = string.Empty;

    public int MaxPlayers { get; set; }

    public Guid PlayerId { get; set; }

    public string PlayerName { get; set; } = string.Empty;
}
