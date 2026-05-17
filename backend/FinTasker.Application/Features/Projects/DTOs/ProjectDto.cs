using System;
using FinTasker.Domain.Enums;

namespace FinTasker.Application.Features.Projects.DTOs
{

    public record ProjectDto
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusProjects Status { get; set; }
        public string Color { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

    }
}

