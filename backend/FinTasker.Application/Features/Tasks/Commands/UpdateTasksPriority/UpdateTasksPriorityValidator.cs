using FluentValidation;

namespace FinTasker.Application.Features.Tasks.Commands.UpdateTasksPriority
{

    public class UpdateTasksPriorityValidator : AbstractValidator<UpdateTasksPriorityCommand>
    {
        public UpdateTasksPriorityValidator()
        {
            RuleFor(x => x.Priority)
            .IsInEnum()
            .WithMessage("The entered priority is invalid");
        }

    }


}
