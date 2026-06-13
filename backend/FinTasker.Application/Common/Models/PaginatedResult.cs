using System;

namespace FinTasker.Application.Common.Models
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; init; } = [];
    }

}

