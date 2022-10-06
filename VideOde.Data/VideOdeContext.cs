using Microsoft.EntityFrameworkCore;
using VideOde.Core;

namespace VideOde.Data;
public class VideOdeContext : DbContext
{
    public VideOdeContext(DbContextOptions<VideOdeContext> options) : base (options) { }

    public DbSet<Clip> Clips { get; set; }
}
