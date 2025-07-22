using FluentValidation;

namespace Application.Menus.Queries.GetMenuPaginate;

public class GetMenuPaginateQueryValidator : AbstractValidator<GetMenuPaginateQuery>
{
   public GetMenuPaginateQueryValidator()
   {
      RuleFor(x => x.pageNumber).GreaterThan(0);
      RuleFor(x => x.pageSize).GreaterThan(0);
   }
}