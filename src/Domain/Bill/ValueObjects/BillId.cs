using Domain.Common.Models;

namespace Domain.Bill.ValueObjects;

public sealed class BillId : AggregateRootId<Guid>
{
   public override Guid Value { get; protected set; }

   private BillId(Guid value)
   {
      Value = value;
   }

   public static BillId CreateUnique()
   {
      //TODO: enforce invariants
      return new BillId(Guid.NewGuid());
   }
   public static BillId Create(Guid value)
   {
      //TODO: enforce invariants
      return new BillId(value);
   }

   public override IEnumerable<object> GetEqualityComponents()
   {
      yield return Value;
   }
}
