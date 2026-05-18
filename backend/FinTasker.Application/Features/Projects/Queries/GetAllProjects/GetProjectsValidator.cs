using FinTasker.Application.Common.Models;
using FluentValidation;

namespace FinTasker.Application.Features.Projects.Queries.GetAllProjects
{
    public class GetProjectsValidator : AbstractValidator<PaginationQuery>
    {
        public GetProjectsValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("PageSize must be between 1 and 100.");
        }
    }


}

