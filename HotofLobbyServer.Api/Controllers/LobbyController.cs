using HotofLobbyServer.Api.Models;
using HotofLobbyServer.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotofLobbyServer.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class LobbyController : ControllerBase
{
    private readonly LobbyService lobbyService;

    public LobbyController(LobbyService lobbyService)
    {
        this.lobbyService = lobbyService;
    }

    [HttpGet]
    public IActionResult GetLobbies()
    {
        return Ok(lobbyService.GetLobbies());
    }

    [HttpGet("{id}")]
    public IActionResult GetLobby(Guid id)
    {
        Lobby? lobby = lobbyService.GetLobby(id);

        if(lobby == null)
            return NotFound();

        return Ok(lobby);
    }

    [HttpPost]
    public IActionResult CreateLobby([FromBody] CreateLobbyRequest request)
    {
        var lobby = lobbyService.CreateLobby(request);

        return Ok(lobby);
    }

    [HttpPost("{id}/join")]
    public IActionResult JoinLobby(Guid id, [FromBody] JoinLobbyRequest request)
    {
        LobbyResponse? response = lobbyService.JoinLobby(id, request);

        if (response == null)
            return BadRequest("Unable to join lobby");

        return Ok(response);

    }

    [HttpPost("{id}/leave")]
    public IActionResult LeaveLobby(Guid id, [FromBody] LeaveLobbyRequest request)
    {
        Lobby? lobby = lobbyService.LeaveLobby(id, request);

        if (lobby == null)
            return NotFound();

        return Ok(lobby);
    }

    [HttpPost("{id}/ready")]
    public IActionResult Ready(Guid id, [FromBody] ReadyRequest request)
    {
        Lobby? lobby = lobbyService.GetLobby(id);

        if(lobby == null)
            return NotFound();

        LobbyPlayer? player = lobby.Players.FirstOrDefault(p => p.Id == request.PlayerId);

        if (player == null)
            return NotFound();

        player.isReady = request.IsReady;

        return Ok(lobby);
    }

    [HttpPost("{id}/start")]
    public IActionResult StartGame(Guid id)
    {
        Lobby? lobby = lobbyService.GetLobby(id);

        if (lobby == null)
            return NotFound();

        LobbyPlayer? host = lobby.Players.FirstOrDefault(p => p.isHost);

        if (host == null)
            return BadRequest("No host found");

        //NOTE: Not using this in testing, but is useful for actual game
        //if (lobby.Players.Any(p => !p.isReady))
        //    return BadRequest("Not all players ready");

        lobby.InGame = true;

        return Ok(lobby);
    }

    [HttpPost("{id}/heartbeat")]
    public IActionResult Heartbeat(Guid id)
    {
        bool success = lobbyService.Heartbeat(id);

        if (!success)
            return NotFound();

        return Ok();
    }
}
