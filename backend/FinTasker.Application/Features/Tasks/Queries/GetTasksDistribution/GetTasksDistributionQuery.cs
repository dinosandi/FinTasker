using System;
using FinTasker.Application.Features.Tasks.DTOs;
using MediatR;

namespace FinTasker.Application.Features.Tasks.Queries
{
    public sealed record GetTasksDistributionQuery() : IRequest<TasksDistributionDto>;

}
