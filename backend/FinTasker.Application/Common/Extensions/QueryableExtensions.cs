using FinTasker.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace FinTasker.Application.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(
            this IQueryable<T> query,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return PaginatedResult<T>.Create(items, totalCount, page, pageSize);
        }

        public static Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(
            this IQueryable<T> query,
            PaginationQuery pagination,
            CancellationToken cancellationToken = default)
            => query.ToPaginatedResultAsync(pagination.Page, pagination.PageSize, cancellationToken);
    }
}