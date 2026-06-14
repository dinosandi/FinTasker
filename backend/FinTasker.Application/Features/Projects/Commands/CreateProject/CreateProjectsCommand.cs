using System;
using MediatR;
using FinTasker.Domain.Enums;
using FinTasker.Application.Common.Models;

namespace FinTasker.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectsCommand 
    : IRequest<ApiResponse<CreateProjectsResponse>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusProjects Status { get; set; }
        public string Color { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
    }
    
    public class CreateProjectsResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
    }
}

