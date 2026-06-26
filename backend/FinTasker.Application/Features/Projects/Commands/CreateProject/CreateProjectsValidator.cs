using System.Data;
using FluentValidation;

namespace FinTasker.Application.Features.Projects.Commands.CreateProject
{

    public class CreateProjectsValidator : AbstractValidator<CreateProjectsCommand>
    {
        public CreateProjectsValidator()
        {
            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("End date must be greater than start date.");
            RuleFor(x => x.Description)
                .MaximumLength(350);
        }

    }


}
