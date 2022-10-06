using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VideOde.Core;
using VideOde.Data.Providers;

namespace VideOde.Data.Tests
{
    [TestClass]
    public class ClipProviderTests
    {
        [TestMethod]
        public void AddAndGet()
        {
            var services = ConfigureServices();
            var provider = services.GetRequiredService<IClipService>();

            Clip testClip = GetClipForTesting();

            provider.AddClip(testClip);

            Clip clipFromDatabase = provider.GetClipByTitle(testClip.Title);
            Assert.IsNotNull(clipFromDatabase);
        }

        [TestMethod]
        public void AddRange()
        {
            var services = ConfigureServices();
            var provider = services.GetRequiredService<IClipService>();

            List<Clip> clipsList = new();

            for (int i = 0; i < 10; i++)
            {
                Clip testClip = GetClipForTesting();
                clipsList.Add(testClip);
            }

            provider.AddClipRange(clipsList);

            foreach (Clip c in clipsList)
            {
                IEnumerable<Clip> clipsFromDb = provider.GetAllClips().Where(i => i.Title == c.Title);
                Assert.AreEqual(clipsFromDb.Count(), 1);
            }
        }

        [TestMethod]
        public void Remove()
        {
            var services = ConfigureServices();
            var provider = services.GetRequiredService<IClipService>();

            Clip testClip = GetClipForTesting();

            provider.AddClip(testClip);

            provider.RemoveClip(testClip);

            IEnumerable<Clip> clipFromDb = provider.GetAllClips().Where(c => c.Title == testClip.Title);
            Assert.AreEqual(clipFromDb.Count(), 0);
        }

        [TestMethod]
        public void GetWithCount()
        {
            var services = ConfigureServices();
            var provider = services.GetRequiredService<IClipService>();

            List<Clip> clipsList = new();

            for (int i = 0; i < 10; i++)
            {
                Clip testClip = GetClipForTesting();
                clipsList.Add(testClip);
            }

            provider.AddClipRange(clipsList);

            const int numberToGet = 5;

            IEnumerable<Clip> clipsFromDb = provider.GetClips(numberToGet);

            Assert.AreEqual(clipsFromDb.Count(), numberToGet);
        }

        private static Clip GetClipForTesting()
        {
            Guid titleGuid = Guid.NewGuid();
            string titleString = titleGuid.ToString();

            return new Clip
            {
                Title = titleString,
            };
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            var conn = new SqliteConnection("Filename=:memory:");
            conn.Open();

            var context = new VideOdeContext(new DbContextOptionsBuilder<VideOdeContext>()
                          .UseSqlite(conn).Options);

            context.Database.EnsureCreated();

            services.AddSingleton(context);
            services.AddSingleton<IClipService, ClipService>();

            return services.BuildServiceProvider();
        }
    }
}