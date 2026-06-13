namespace FinTasker.Application.Common.Models
{
    public class PaginationMeta
    {
        public int Page { get; init; }

        public int PageSize { get; init; }

        public int TotalCount { get; init; }

        public int TotalPages { get; init; }

        public bool HasNextPage { get; init; }

        public bool HasPreviousPage { get; init; }

        public DateTimeOffset Timestamp { get; init; }
            = DateTimeOffset.UtcNow;
    }
}