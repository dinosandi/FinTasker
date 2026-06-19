namespace FinTasker.Application.Common.Models
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; init; } = [];
        public PaginationMeta Meta { get; init; } = new();

        public static PaginatedResult<T> Create(
            List<T> items,
            int totalCount,
            int page,
            int pageSize)
        {
            return new PaginatedResult<T>
            {
                Items = items,
                Meta = PaginationMeta.From(totalCount, page, pageSize)
            };
        }
    }
}