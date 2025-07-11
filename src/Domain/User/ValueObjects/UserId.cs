using System.Runtime.InteropServices;
using Domain.Common.Models;

namespace Domain.User.ValueObjects;

public sealed class UserId : AggregateRootId<Guid>
{
   public override Guid Value {get; protected set;}

   private UserId(Guid value)
   {
      Value = value;
   }
   public static UserId CreateUnique() => new UserId(Guid.NewGuid());

   public static UserId Create(Guid value) => new UserId(value);

   public override IEnumerable<object> GetEqualityComponents()
   {
      yield return Value;
   }
}
