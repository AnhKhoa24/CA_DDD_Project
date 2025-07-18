using Application.Menus.Common;
using MediatR;

namespace Application.Menus.Queries.GetMenuPaginate;

public record GetMenuPaginateQuery(int pageNumber = 1, int pageSize = 10): IRequest<MenuResult>;