namespace API.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public T Data { get; set; }

        public ApiResponse() { }

        public ApiResponse(T data)
        {
            Data = data;
            Success = true;
        }

        public static ApiResponse<T> Fail() => new ApiResponse<T> { Success = false };
    }

}
