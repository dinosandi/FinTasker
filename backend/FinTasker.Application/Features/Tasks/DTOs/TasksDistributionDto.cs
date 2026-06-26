
namespace FinTasker.Application.Features.Tasks.DTOs
{
    public sealed class TasksDistributionDto
    {
        public int Total { get; init; }
        public int Todo { get; init; }
        public int InProgress { get; init; }
        public int Review { get; init; }
        public int Completed { get; init; }
        public int Cancelled { get; init; }
    }

}
