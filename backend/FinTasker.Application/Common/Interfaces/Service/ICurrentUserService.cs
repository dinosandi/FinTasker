using System;

namespace FinTasker.Application.Common.Interfaces.Service
{
    public interface ICurrentUserService
    {
        Guid GetCurrentUserId();
    }
}

