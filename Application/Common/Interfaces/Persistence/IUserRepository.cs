using Domain.Entities;
namespace Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    void AddUser(User user);
}