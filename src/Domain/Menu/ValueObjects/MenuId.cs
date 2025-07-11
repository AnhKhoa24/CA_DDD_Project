using Domain.Common.Models;

namespace Domain.Menu.ValueObjects;


public sealed class MenuId : AggregateRootId<Guid>
{
   public override Guid Value { get; protected set; }

   private MenuId(Guid value)
   {
      Value = value;
   }

   public static MenuId CreateUnique()
   {
      //TODO: enforce invariants
      return new MenuId(Guid.NewGuid());
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