using Application.Common.Interfaces.Persistence;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
   private readonly KhoaDinnerDbContext _context;

   public UserRepository(KhoaDinnerDbContext context)
   {
      _context = context;
   }

   public async Task AddUser(User user)
   {
      await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();
   }

   public async Task<User?> GetUserByEmailAsync(string email)
   {
      return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
   }
}
