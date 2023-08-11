using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Sweets.Models
{
  public class SweetsContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Treat> Treats { get; set; }
    public DbSet<Flavor> Flavors { get; set; }
    public DbSet<TreatFlavor> TreatFlavors { get; set; }

    public SweetsContext(DbContextOptions options) : base(options) { }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TreatFlavor>()
                .HasKey(tf => new { tf.TreatId, tf.FlavorId });  // Assuming your join entity has TreatId and FlavorId as the composite key

            modelBuilder.Entity<TreatFlavor>()
                .HasOne(tf => tf.Treat)
                .WithMany(t => t.JoinEntities)
                .HasForeignKey(tf => tf.TreatId);

            modelBuilder.Entity<TreatFlavor>()
                .HasOne(tf => tf.Flavor)
                .WithMany(f => f.JoinEntities)
                .HasForeignKey(tf => tf.FlavorId);
        }
  }
}