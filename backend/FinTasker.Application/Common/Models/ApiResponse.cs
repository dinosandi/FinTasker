namespace FinTasker.Application.Common.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }

        public string Message { get; init; } = string.Empty;

        public T? Data { get; init; }

        public object? Meta { get; init; }

        public List<string>? Errors { get; init; }

        public static ApiResponse<T> SuccessResponse(
            T data,
            object? meta = null,
            string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Meta = meta
            };
        }

        public static ApiResponse<T> Fail(
            string message,
            List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }
    }
}