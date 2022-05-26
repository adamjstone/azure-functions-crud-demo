using AdamStone.AzureFunctionsCrudDemo.Domain.DataPersistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdamStone.AzureFunctionsCrudDemo.Functions
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration(config =>
                {
                    config
                        .AddJsonFile("appsettings.json", false, false)
                        .AddEnvironmentVariables()
                        .Build();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<PetDbContext>(options =>
                    {
                        options.UseCosmos("PetDbContext", "Pets");
                    });
                })
                .ConfigureFunctionsWorkerDefaults()
                .Build();

            host.Run();
        }
    }
}