using Domain.Common.Models;
using Domain.User.ValueObjects;

namespace Domain.User;

public sealed class User : AggregateRoot<UserId, Guid>
{
   public string FirstName { get; private set; }
   public string LastName { get; private set; }
   public string Email { get; private set; }
   public string Password { get; private set; }
   public DateTime CreatedAt { get; private set; }
   public DateTime UpdatedAt { get; private set; }

   private User(UserId userId, string firstName, string lastName, string email, string password, DateTime createdAt, DateTime updatedAt)
   : base(userId)
   {
      FirstName = firstName;
      LastName = lastName;
      Email = email;
      Password = password;
      CreatedAt = createdAt;
      UpdatedAt = updatedAt;
   }
   public static User Create(string firstName, string lastName, string email, string password)
   {
      return new(
         UserId.CreateUnique(),
         firstName,
         lastName,
         email,
         password,
         DateTime.UtcNow,
         DateTime.UtcNow
      );
   }

#pragma warning disable CS8618
   private User() { }
#pragma warning restore CS8618

}