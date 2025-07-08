using Domain.Menu;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class KhoaDinnerDbContext : DbContext
{
   public KhoaDinnerDbContext(DbContextOptions<KhoaDinnerDbContext> options)
      : base(options)
   {

   }

   public DbSet<Menu> Menus { get; set; } = null!;

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder
         .ApplyConfigurationsFromAssembly(typeof(KhoaDinnerDbContext).Assembly);

      base.OnModelCreating(modelBuilder);
   }
}