using Microsoft.Extensions.DependencyInjection;
using VideOde.Core;
using VideOde.Data.Providers;
using VideOde.Terminal;

class Program
{
    public static void Main()
    {
        var services = ConfigureServices();

        var clipService = services.GetRequiredService<IClipService>();

        IEnumerable<Clip> clips = clipService.GetAllClips();

        string defaultTableValue = string.Empty;

        List<IEnumerable<string>> cols = new()
        {
            clips.Select(clip => clip.Title),
            clips.Select(clip => clip ?.Description ?? defaultTableValue),
            clips.Select(clip => clip.StartDate?.ToString() ?? defaultTableValue),
            clips.Select(clip => clip.EndDate?.ToString() ?? defaultTableValue),
            clips.Select(clip => clip.Length.ToString())
        };

        IEnumerable<string> headings = new List<string>()
        {
            nameof(Clip.Title),
            nameof(Clip.Description),
            nameof(Clip.StartDate),
            nameof(Clip.EndDate),
            nameof(Clip.Length)
        };

        var table = new TabularData(cols, headings);

        IEnumerable<string> printableRows = table.GetPrintableRows();

        foreach (string row in printableRows)
        {
            Console.WriteLine(row);
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