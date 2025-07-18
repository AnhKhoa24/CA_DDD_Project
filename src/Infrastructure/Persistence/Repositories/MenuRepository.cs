using Application.Common.Interfaces.Persistence;
using Domain.Menu;
using Domain.Menu.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class MenuRepository : IMenuRepository
{
   private readonly KhoaDinnerDbContext _dbContext;

   public MenuRepository(KhoaDinnerDbContext dbContext)
   {
      _dbContext = dbContext;
   }

   public async Task AddMenuAsync(Menu menu)
   {
      await _dbContext.Menus.AddAsync(menu);
      await _dbContext.SaveChangesAsync();
   }

   public async Task<Menu?> GetMenuByIdAsync(MenuId menuId)
   {
      return await _dbContext.Menus.FirstOrDefaultAsync(x => x.Id == menuId);
   }

   public async Task<List<Menu>> GetMenusPaginateAsync(int pageNumber, int pageSize)
   {
      return await _dbContext.Menus
         .OrderBy(m => m.Id)
         .Skip((pageNumber - 1) * pageSize)
         .Take(pageSize)
         .ToListAsync();
   }

   public async Task SaveChangesAsync()
   {
      await _dbContext.SaveChangesAsync();
   }
}
