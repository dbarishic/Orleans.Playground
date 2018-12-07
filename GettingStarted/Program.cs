using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using static System.Console;

namespace GettingStarted
{
    class Program
    {
        static async Task Main(string[] args)
        {
           var siloBuilder = new SiloHostBuilder()
                .UseLocalhostClustering()
              //  .UseDashboard(options => {})
                .Configure<ClusterOptions>(options => 
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "GettingStartedOrleans";
                })
                .Configure<EndpointOptions>(options =>
                    options.AdvertisedIPAddress = System.Net.IPAddress.Loopback)
                    .ConfigureLogging(log => log.AddConsole());

            var clientBuilder = new ClientBuilder()
                    .UseLocalhostClustering()
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "GettingStartedOrleans";
                    })
                    .ConfigureLogging(log => log.AddConsole());

            using (var host = siloBuilder.Build())
            using (var client = clientBuilder.Build())
            {
                await host.StartAsync();
                await client.Connect();

                var random = new Random();

                while (true)
                {
                    int grainId = random.Next(0, 500);
                    float temperature = (float)random.NextDouble() * 40;
                    var sensor = client.GetGrain<ITemperatureSensorGrain>(grainId);
                    await sensor.SubmitTemperatureAsync(temperature);
                }
            }    
        }
    }
}
