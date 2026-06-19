using FluentValidation;

namespace FinTasker.Application.Features.Tasks.Commands.CreateTasks
{
    public class CreateTasksValidator : AbstractValidator<CreateTasksCommand>
    {
        public CreateTasksValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("ProjectId is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("Invalid priority value.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid status value.");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTimeOffset.UtcNow).WithMessage("Due date must be in the future.");
        }
    }
}
