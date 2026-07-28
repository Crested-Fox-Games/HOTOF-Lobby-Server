namespace HotofLobbyServer.Api.Models;

public class LobbyPlayer
{
    /// <summary>
    /// Unique Id for this player
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Player display name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Whether this player is the owner of the lobby
    /// </summary>
    public bool isHost { get; set; }

    /// <summary>
    /// Used for if we want all players to ready up
    /// </summary>
    public bool isReady { get; set; }
}