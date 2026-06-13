using FluentValidation;

namespace FinTasker.Application.Features.Tasks.Queries.GetFilteredTasks
{
    public class GetFilteredTasksValidator : AbstractValidator<GetFilteredTasksQuery>
    {
        public GetFilteredTasksValidator()
        {
            // Pagination
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page harus dimulai dari 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("PageSize harus antara 1 dan 100.");

        }
    }
}