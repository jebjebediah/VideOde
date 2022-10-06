using Microsoft.EntityFrameworkCore;
using VideOde.Core;

namespace VideOde.Data;
public class VideOdeContext : DbContext
{
    public DbSet<Clip> Clips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\VideOde\\VideOde.db");
    }
}
