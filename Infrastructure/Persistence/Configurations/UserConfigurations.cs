using Domain.User;
using Domain.User.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
   public void Configure(EntityTypeBuilder<User> builder)
   {
      ConfigureUserTable(builder);
   }

   private void ConfigureUserTable(EntityTypeBuilder<User> builder)
   {
      builder.ToTable("Users");

      builder.HasKey(t => t.Id);

      builder.Property(x => x.Id)
         .ValueGeneratedNever()
         .HasConversion(
            id => id.Value,
            value => UserId.Create(value)
         );

      builder.Property(x => x.FirstName)
         .HasMaxLength(50);

      builder.Property(x => x.LastName)
         .HasMaxLength(50);

      builder.Property(x => x.Email)
         .HasMaxLength(100);

      builder.Property(x => x.Password)
         .HasMaxLength(255);
   }
}
