
using HotofLobbyServer.Api.Services;

public class LobbyCleanupService : BackgroundService
{
    private readonly LobbyService lobbyService;

    public LobbyCleanupService(LobbyService lobbyService)
    {
        this.lobbyService = lobbyService;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            lobbyService.CleanupLobbies();

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        }
    }
}
