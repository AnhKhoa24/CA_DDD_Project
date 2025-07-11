using Domain.Common.Models;
using Domain.Menu.Entities;
using Domain.Menu.Events;
using Domain.Menu.ValueObjects;

namespace Domain.Menu;


public sealed class Menu : AggregateRoot<MenuId, Guid>
{
   private readonly List<MenuSection> _sections = new();
   public string Name { get; private set; }
   public string Description { get; private set; }
   public float AverageRating { get; private set; }

   public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();

   private Menu(MenuId menuId, string name, string description, List<MenuSection> sections)
   : base(menuId)
   {
      Name = name;
      Description = description;
      _sections = sections; 
   }

   public static Menu Create(string name, string description, List<MenuSection>? sections)
   {
      var menu = new Menu(
         MenuId.CreateUnique(),
         name,
         description,
         sections ?? new()
      );

      menu.AddDomainEvent(new MenuCreated(menu));

      return menu;
   }

#pragma warning disable CS8618
   private Menu()
   {

   }
#pragma warning restore CS8618

}