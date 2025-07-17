using Domain.Menu;
using Domain.Menu.ValueObjects;

namespace Application.Common.Interfaces.Persistence;

public interface IMenuRepository
{
   Task AddMenuAsync(Menu menu);
   Task<List<Menu>> GetMenusPaginateAsync(int pageNumber = 1, int pageSize = 10);
   Task SaveChangesAsync();
   Task<Menu?> GetMenuByIdAsync(MenuId menuId);
}