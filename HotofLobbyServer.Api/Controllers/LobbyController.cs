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

    [HttpPost]
    public IActionResult CreateLobby(CreateLobbyRequest request)
    {
        var lobby = lobbyService.CreateLobby(
            request.Name,
            request.HostIp,
            request.MaxPlayers
            );

        return Ok(lobby);
    }

    [HttpPost("{id}/join")]
    public IActionResult JoinLobby(Guid id)
    {
        Lobby? lobby = lobbyService.JoinLobby(id);

        if (lobby == null)
            return BadRequest("Unable to join lobby");

        return Ok(lobby);

    }

    [HttpPost("{id}/leave")]
    public IActionResult LeaveLobby(Guid id)
    {
        bool success = lobbyService.LeaveLobby(id);

        if (!success)
            return BadRequest("Lobby not found");

        return Ok();
    }
}

public class CreateLobbyRequest
{
    public string Name { get; set; } = string.Empty;

    public string HostIp {  get; set; } = string.Empty;

    public int MaxPlayers { get; set; }
}