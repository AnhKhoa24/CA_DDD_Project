using Domain.Menu;
using Domain.Menu.Entities;
using Domain.Menu.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MenuConfigurations : IEntityTypeConfiguration<Menu>
{
   public void Configure(EntityTypeBuilder<Menu> builder)
   {
      ConfigureMenuTable(builder);
      ConfigureMenuSectionTable(builder);
   }

   private void ConfigureMenuSectionTable(EntityTypeBuilder<Menu> builder)
   {
      builder.OwnsMany(m => m.Sections, sb =>
      {
         sb.ToTable("MenuSections");

         sb.WithOwner().HasForeignKey("MenuId");

         sb.HasKey("Id", "MenuId");

         sb.Property(s => s.Id)
            .HasColumnName("MenuSectionId")
            .ValueGeneratedNever()
            .HasConversion(
               id => id.Value,
               value => MenuSectionId.Create(value));

         sb.Property(s => s.Name).HasMaxLength(100);

         sb.Property(s => s.Description).HasMaxLength(100);

         sb.OwnsMany(s => s.Items, ib =>
         {
            ib.ToTable("MenuItems");

            ib.WithOwner().HasForeignKey("MenuSectionId", "MenuId");

            ib.HasKey(nameof(MenuItem.Id), "MenuSectionId", "MenuId");

            ib.Property(i => i.Id)
               .ValueGeneratedNever()
               .HasConversion(
                  id => id.Value,
                  value => MenuItemId.Create(value)
               );

            ib.Property(s => s.Name).HasMaxLength(100);

            ib.Property(s => s.Description).HasMaxLength(100);
         });

         sb.Navigation(s => s.Items).Metadata.SetField("_items");
         sb.Navigation(s => s.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
      });
      builder.Metadata.FindNavigation(nameof(Menu.Sections))!
         .SetPropertyAccessMode(PropertyAccessMode.Field);
   }

   private void ConfigureMenuTable(EntityTypeBuilder<Menu> builder)
   {
      builder.ToTable("Menus"); //Table name

      builder.HasKey(x => x.Id); //Primary key

      builder.Property(x => x.Id)
         .ValueGeneratedNever()
         .HasConversion(
            id => id.Value,
            value => MenuId.Create(value));

      builder.Property(x => x.Name)
         .HasMaxLength(100);

      builder.Property(x => x.Description)
         .HasMaxLength(100);

   }
}
