using Application.Common.Interfaces.Persistence;
using Domain.Menu;

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
}
