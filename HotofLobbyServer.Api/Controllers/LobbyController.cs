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
        Lobby? lobby = lobbyService.JoinLobby(id, request);

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
