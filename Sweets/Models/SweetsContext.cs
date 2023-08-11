using Microsoft.EntityFrameworkCore;

namespace Sweets.Models
{
  public class SweetsContext : DbContext
  {
    public virtual DbSet<Treat> Treats { get; set; }
    public DbSet<Flavor> Flavors { get; set; }
    public DbSet<TreatFlavor> TreatFlavors { get; set; }

    public SweetsContext(DbContextOptions options) : base(options) { }


  }
}