using FluentValidation;

namespace FinTasker.Application.Features.Tasks.Commands.UpdateTasks
{
    public class UpdateTasksValidator
        : AbstractValidator<UpdateTasksCommand>
    {
        public UpdateTasksValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.Estimed_Minutes)
                .GreaterThanOrEqualTo(0).WithMessage("Estimated minutes must be greater than or equal to 0.");
            
            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(DateTimeOffset.UtcNow).WithMessage("Due date must be in the future.")
                .When(x => x.DueDate.Date != default);

        }
    }
}
