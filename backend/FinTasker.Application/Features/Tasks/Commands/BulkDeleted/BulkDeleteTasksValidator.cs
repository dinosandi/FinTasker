using FluentValidation;


namespace FinTasker.Application.Features.Tasks.Commands.BulkDeleted
{
    public sealed class BulkDeleteTasksValidator : AbstractValidator<BulkDeleteTasksCommand>
    {
        public BulkDeleteTasksValidator()
        {
            RuleFor(x => x.TaskIds)
                .NotNull().WithMessage("TaskIds is required.")
                .NotEmpty().WithMessage("TaskIds must not be empty.");

            RuleFor(x => x.TaskIds.Count)
                .LessThanOrEqualTo(100)
                .WithMessage("Cannot delete more than 100 tasks at once.");

            RuleForEach(x => x.TaskIds)
                .NotEqual(Guid.Empty)
                .WithMessage("Each TaskId must be a valid GUID.");
        }

    }
}

