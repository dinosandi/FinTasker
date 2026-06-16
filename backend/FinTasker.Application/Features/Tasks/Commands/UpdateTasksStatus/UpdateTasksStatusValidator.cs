using FluentValidation;

namespace FinTasker.Application.Features.Tasks.Commands.UpdateTasksStatus
{

    public class UpdateTasksStatusValidator : AbstractValidator<UpdateTasksStatusCommand>
    {
        public UpdateTasksStatusValidator()
        {
            RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("The entered status is invalid");
        }

    }


}
