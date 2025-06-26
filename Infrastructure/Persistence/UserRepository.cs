using Application.Common.Interfaces.Persistence;
using Domain.Entities;

namespace Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static List<User> _users = new List<User>();
    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(x => x.Email == email);
    }
}
