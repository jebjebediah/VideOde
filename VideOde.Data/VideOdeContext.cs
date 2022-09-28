using Microsoft.EntityFrameworkCore;

namespace VideOde.Data;
public class VideOdeContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\VideOde\\VideOde.db");
    }
}
