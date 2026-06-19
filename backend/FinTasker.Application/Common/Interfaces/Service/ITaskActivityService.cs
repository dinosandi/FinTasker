using System;
using FinTasker.Domain.Enums;

namespace FinTasker.Application.Common.Interfaces.Service
{

    public interface ITaskActivityService
    {
        Task LogAsync(
            Guid Id,
            ActivityType activityType,
            string description,
            CancellationToken ct = default
        );

    }


}
