namespace CourseInside.Models
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        protected ServiceResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static ServiceResult Ok(string message = "") => new ServiceResult(true, message);
        public static ServiceResult Fail(string message) => new ServiceResult(false, message);
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        protected ServiceResult(bool success, T? data, string message) : base(success, message)
        {
            Data = data;
        }

        public static ServiceResult<T> Ok(T data, string message = "") => new ServiceResult<T>(true, data, message);
        public static ServiceResult<T> Fail(string message) => new ServiceResult<T>(false, default, message);
    }
}
