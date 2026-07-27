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
}

public class CreateLobbyRequest
{
    public string Name { get; set; } = string.Empty;

    public string HostIp {  get; set; } = string.Empty;

    public int MaxPlayers { get; set; }
}