namespace HotofLobbyServer.Api.Models;
public class JoinLobbyRequest
{
    public Guid PlayerId { get; set; }

    public string PlayerName { get; set; } = string.Empty;
}
