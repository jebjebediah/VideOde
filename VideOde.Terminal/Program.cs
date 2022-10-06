using Microsoft.Extensions.DependencyInjection;
using VideOde.Core;
using VideOde.Data.Providers;

class Program
{
    public static void Main()
    {
        var services = ConfigureServices();

        var clipService = services.GetRequiredService<IClipService>();

        clipService.AddClip(new Clip
        {
            Title = "Initial clip",
            Length = 0
        });

        Console.WriteLine(clipService.GetClipCount());
    }

    public static ServiceProvider ConfigureServices()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddDatabaseContext();
        serviceCollection.AddDataProviders();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        return serviceProvider;
    }
}