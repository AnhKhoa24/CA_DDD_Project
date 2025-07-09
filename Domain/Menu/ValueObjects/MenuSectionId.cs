using Domain.Common.Models;

namespace Domain.Menu.ValueObjects;


public sealed class MenuSectionId : ValueObject
{
   public Guid Value { get; set; }

   private MenuSectionId(Guid value)
   {
      Value = value;
   }

   public static MenuSectionId CreateUnique()
   {
      return new(Guid.NewGuid());
   }
   public static MenuSectionId Create(Guid value)
   {
      //TODO: enforce invariants
      return new MenuSectionId(value);
   }

   public override IEnumerable<object> GetEqualityComponents()
   {
      yield return Value;
   }
}