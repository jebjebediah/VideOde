using Microsoft.Extensions.DependencyInjection;
using VideOde.Data.Providers;
using VideOde.Terminal;

class Program
{
    public static void Main()
    {
        var services = ConfigureServices();

        while (true)
        {
            Console.Write("Enter command: ");
            string input = Console.ReadLine() ?? "";
            List<string> inputList = input.Split(' ').ToList();

            string? area = inputList.ElementAtOrDefault(0);
            string? verb = inputList.ElementAtOrDefault(1);
            string? arg1 = inputList.ElementAtOrDefault(2);

            if (area == "clips")
            {
                ClipsArea.Handle(services.GetRequiredService<IClipService>(), verb, arg1);
            }
            else if (area == "exit")
            {
                return;
            }
            else
            {
                Console.WriteLine("Input not recognized!");
            }
        }
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