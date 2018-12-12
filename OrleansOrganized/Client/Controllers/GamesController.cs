using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using OrleansOrganized.Contracts;

[Produces("application/json")]
[Route("api/Games")]
public class GamesController : Controller
{
    private IClusterClient _orleansClient;

    public GamesController(IClusterClient orleansClient)
    {
        _orleansClient = orleansClient;
    }

    [HttpGet]
    public Task<List<string>> Get(int gameId)
    {
        var grain = this._orleansClient.GetGrain<IGameGrain>(gameId);
        return grain.ListPlayersAsync();
    }

    [HttpPut]
    public async Task Put(int gameId, string playerName)
    {
        var grain = this._orleansClient.GetGrain<IGameGrain>(gameId);
        await grain.JoinAsync(playerName);
    }

    [HttpDelete]
    public async Task Delete(int gameId, string playerName)
    {
        var grain = this._orleansClient.GetGrain<IGameGrain>(gameId);
        await grain.LeaveAsync(playerName);
    }
}