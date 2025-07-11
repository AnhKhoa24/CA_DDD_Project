using Domain.Common.Models;

namespace Domain.Menu.ValueObjects;


public sealed class MenuItemId : ValueObject
{
   public Guid Value { get; set; }

   private MenuItemId(Guid value)
   {
      Value = value;
   }

   public static MenuItemId CreateUnique()
   {
      return new(Guid.NewGuid());
   }
   public static MenuItemId Create(Guid value)
   {
      //TODO: enforce invariants
      return new MenuItemId(value);
   }

   public override IEnumerable<object> GetEqualityComponents()
   {
      yield return Value;
   }
}