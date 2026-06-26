using FluentValidation;

namespace FinTasker.Application.Features.Tasks.Queries.GetTodayTasks
{

    public sealed class GetTodayTasksValidator : AbstractValidator<GetTasksDistributionQuery>
    {
        public GetTodayTasksValidator() { }
    }

}
