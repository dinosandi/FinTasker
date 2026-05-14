using System;

namespace FinTasker.Application.Common.Interfaces.Service
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? Email { get; }
        bool IsAuthenticated { get; }
    }

}

