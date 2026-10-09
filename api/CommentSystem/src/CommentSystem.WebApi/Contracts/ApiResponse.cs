namespace CommentSystem.WebApi.Contracts
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }

        public static ApiResponse Fail(string message, List<string>? errors = null)
        {
            return new ApiResponse { Success = false, Message = message, Errors = errors };
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string? message = null)
        {
            return new ApiResponse<T> { Success = true, Data = data, Message = message };
        }

        public static new ApiResponse<T> Fail(string message, List<string>? errors = null)
        {
            return new ApiResponse<T> { Success = false, Message = message, Errors = errors };
        }
    }
}
