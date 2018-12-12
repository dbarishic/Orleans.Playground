using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orleans;
using OrleansOrganized.Contracts;

public class GameGrain : Grain, IGameGrain
{
    private HashSet<string> _players;

    public GameGrain() => this._players = new HashSet<string>();
    public Task JoinAsync(string playerName)
    {
        this._players.Add(playerName);
        return Task.CompletedTask;
    }

    public Task LeaveAsync(string playerName)
    {
        this._players.Remove(playerName);
        return Task.CompletedTask;
    }

    public Task<List<string>> ListPlayersAsync()
        => Task.FromResult(this._players.ToList());
}