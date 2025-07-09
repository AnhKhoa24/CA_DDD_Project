using Domain.Menu;

namespace Application.Common.Interfaces.Persistence;

public interface IMenuRepository
{
   Task AddMenuAsync(Menu menu);
}