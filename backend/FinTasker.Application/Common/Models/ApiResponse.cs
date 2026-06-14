using System.Diagnostics;

namespace FinTasker.Application.Common.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
        public T? Data { get; init; }
        public object? Meta { get; init; }
        public List<string>? Errors { get; init; }
        public string? TraceId { get; init; }

      
        public static ApiResponse<T> Ok(T data, string message = "Success.")
            => Build(true, message, data);

     
        public static ApiResponse<List<TItem>> OkPaginated<TItem>(
            PaginatedResult<TItem> result,
            string message = "Data successfully fetched.")
            => new()
            {
                Success = true,
                Message = message,
                Data    = result.Items,
                Meta    = result.Meta,
                TraceId = Activity.Current?.Id
            };

        public static ApiResponse<T> Created(T data, string message = "Resource created successfully.")
            => Build(true, message, data);

     
        public static ApiResponse<T> Updated(T data, string message = "Resource updated successfully.")
            => Build(true, message, data);

       
        public static ApiResponse<T> Deleted(string message = "Resource deleted successfully.")
            => Build(true, message, default);

        // ─── Fail 

        public static ApiResponse<T> Fail(string message, List<string>? errors = null)
            => new()
            {
                Success = false,
                Message = message,
                Errors  = errors,
                TraceId = Activity.Current?.Id
            };

        public static ApiResponse<T> Fail(string message, string error)
            => Fail(message, [error]);

        // ─── Private builder 

        private static ApiResponse<T> Build(bool success, string message, T? data, object? meta = null)
            => new()
            {
                Success = success,
                Message = message,
                Data    = data,
                Meta    = meta,
                TraceId = Activity.Current?.Id
            };
    }
}