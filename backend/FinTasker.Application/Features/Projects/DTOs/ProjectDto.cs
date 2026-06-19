using FinTasker.Application.Features.Tasks.DTOs;
using FinTasker.Domain.Enums;

namespace FinTasker.Application.Features.Projects.DTOs
{

    public record ProjectDto
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public List<TaskDto> Tasks { get; init; } = [];

    }
}

