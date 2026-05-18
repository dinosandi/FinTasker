using FluentValidation;

namespace FinTasker.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectValidator 
    : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Status)
            .IsInEnum();

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.Color)
            .NotEmpty();

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate)
            .When(x => x.StartDate.HasValue && x.EndDate.HasValue);

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
            .WithMessage("End date must be greater than start date.");
    }
}