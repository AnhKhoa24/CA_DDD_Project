using Domain.Common.Models;
using Domain.Menu;
using Domain.User;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class KhoaDinnerDbContext : DbContext
{
   private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
   public KhoaDinnerDbContext(DbContextOptions<KhoaDinnerDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor)
      : base(options)
   {
      _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
   }

   public DbSet<Menu> Menus { get; set; } = null!;
   public DbSet<User> Users { get; set; } = null!;

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder
         .Ignore<List<IDomainEvent>>()
         .ApplyConfigurationsFromAssembly(typeof(KhoaDinnerDbContext).Assembly);

      base.OnModelCreating(modelBuilder);
   }

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
      base.OnConfiguring(optionsBuilder);
   }
}