using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace VideOde.Data.Providers
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services)
        {
            var context = new VideOdeContext(new DbContextOptionsBuilder<VideOdeContext>()
              .UseSqlite($"Data Source={Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\VideOde\\VideOde.db").Options);


            services.AddSingleton(context);

            return services;
        }

        public static IServiceCollection AddDataProviders(this IServiceCollection services)
        {
            services.AddSingleton<IClipService, ClipService>();

            return services;
        }
    }
}
