using Domain.Bill.ValueObjects;
using Domain.Common.Models;

namespace Domain.Bill;

public class Bill : AggregateRoot<BillId, Guid>
{
   public Price Price { get; private set; }
   public DateTime CreatedDateTime { get; private set; } = DateTime.Now;
   public DateTime UpdatedDateTime { get; private set; } = DateTime.Now;

   private Bill(Price price)
   {
      Price = price;
   }
}