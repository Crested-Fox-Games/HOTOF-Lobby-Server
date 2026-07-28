namespace HotofLobbyServer.Api.Models;
public class ReadyRequest
{
    public Guid PlayerId { get; set; }

    public bool IsReady { get; set; }
}
