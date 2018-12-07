using Orleans;
using System.Threading.Tasks;

namespace GettingStarted
{
    public interface ITemperatureSensorGrain : IGrainWithIntegerKey
    {
        Task SubmitTemperatureAsync(float temperature);
    }
}