using System.Threading.Tasks;
using Orleans;
using static System.Console;

namespace GettingStarted
{
    public class TemperatureSensorGrain : Grain, ITemperatureSensorGrain
    {
        public Task SubmitTemperatureAsync(float temperature)
        {
            long grainId = this.GetPrimaryKeyLong();
            WriteLine($"{grainId} received temperature reading: {temperature}");

            return Task.CompletedTask;    
        }
    }
}