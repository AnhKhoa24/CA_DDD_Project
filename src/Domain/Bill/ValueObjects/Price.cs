using Domain.Common.Models;

namespace Domain.Bill.ValueObjects;

public sealed class Price : ValueObject
{
   public decimal Amount { get; private set; }

   public string Currency { get; private set; }
   private Price(decimal amount, string currency)
   {
      Amount = amount;
      Currency = currency.ToUpperInvariant(); ;
   }
   public static Price Create(decimal amount, string currency)
   {
      if (amount < 0)
         throw new ArgumentException("Amount cannot be negative.", nameof(amount));

      if (string.IsNullOrWhiteSpace(currency))
         throw new ArgumentException("Currency is required.", nameof(currency));
         
      return new Price(amount, currency);
   }

   public override IEnumerable<object> GetEqualityComponents()
   {
      yield return Amount;
      yield return Currency;
   }
}