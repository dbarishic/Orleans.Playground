using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;

namespace OrleansOrganized.Contracts
{
    public interface IGameGrain : IGrainWithIntegerKey
    {
        Task JoinAsync(string playerName);
        Task LeaveAsync(string playerName);
        Task<List<string>> ListPlayersAsync();
    }
}
