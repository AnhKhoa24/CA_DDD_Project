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
   public void UpdateMenuSections(List<MenuSection> sections)
   {
      _sections.Clear();
      _sections.AddRange(sections);
   }
   public void UpdateSections(List<MenuSection> sections)
   {
      List<MenuSection> uniqueSections = UniqueSection(sections);

      var sectionIds = uniqueSections.ToDictionary(i => i.Id.Value);
      _sections.RemoveAll(existing => !sectionIds.ContainsKey(existing.Id.Value));

      sections.ForEach(s =>
      {
         var exists = _sections.FirstOrDefault(f => f.Id.Value == s.Id.Value);
         if (exists is MenuSection)
         {
            exists = s;
         }
         else
         {
            var newMenuSection = MenuSection.Create(s.Name, s.Description, s.Items.ToList());
            _sections.Add(newMenuSection);
         }
      });
   }

   private List<MenuSection> UniqueSection(List<MenuSection> sections)
   {
      return sections
         .GroupBy(s => s.Id.Value)
         .Select(g => g.Last())
         .ToList();
   }

#pragma warning disable CS8618
   private Menu()
   {

   }
#pragma warning restore CS8618

}