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
      var uniqueSections = UniqueSection(sections);

      RemoveSectionNotIn(uniqueSections);

      sections.ForEach(s =>
      {
         var index = _sections.FindIndex(f => f.Id.Value == s.Id.Value);

         if (index >= 0) _sections[index] = s;
         else _sections.Add(MenuSection.Create(s.Name, s.Description, s.Items.ToList()));
      });
   }

   private void RemoveSectionNotIn(List<MenuSection> uniqueSections)
   {
      var sectionIds = uniqueSections.Select(i => i.Id.Value).ToHashSet();
      _sections.RemoveAll(existing => !sectionIds.Contains(existing.Id.Value));
   }

   private List<MenuSection> UniqueSection(List<MenuSection> sections)
   {
      return sections
         .GroupBy(s => s.Id.Value)
         .Select(g => g.Last())
         .ToList();
   }
   public Menu WithName(string name)
   {
      Name = name;
      return this;
   }
   public Menu WithDescription(string description)
   {
      Description = description;
      return this;
   }
   public Menu WithAverageRating(float averageRating)
   {
      AverageRating = averageRating;
      return this;
   }

#pragma warning disable CS8618
   private Menu()
   {

   }
#pragma warning restore CS8618

}