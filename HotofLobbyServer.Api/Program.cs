using HotofLobbyServer.Api.Services;

var builder = WebApplication.CreateBuilder(args);

//Add services
builder.Services.AddControllers();

//Add lobby services as a singleton as we only want one
builder.Services.AddSingleton<LobbyService>();

builder.Services.AddHostedService<LobbyCleanupService>();

var app = builder.Build();

app.MapControllers();

app.Run();

