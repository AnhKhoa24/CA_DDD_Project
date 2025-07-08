using Domain.Common.Models;

namespace Domain.Menu.ValueObjects;


public sealed class MenuId : ValueObject
{
   public Guid Value { get; set; }

   private MenuId(Guid value)
   {
      Value = value;
   }

   public static MenuId CreateUnique()
   {
      //TODO: enforce invariants
      return new(Guid.NewGuid());
   }
   public static MenuId Create(Guid value)
   {
      //TODO: enforce invariants
      return new MenuId(value);
   }

   public override IEnumerable<object> GetEqualityComponents()
   {
      yield return Value;
   }
}