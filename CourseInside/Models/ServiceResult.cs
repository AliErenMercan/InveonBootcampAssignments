public class ServiceResult
{
    public bool success { get; private set; }
    public string? message { get; private set; }

    protected ServiceResult(bool success, string? message = null)
    {
        this.success = success;
        this.message = message;
    }

    public static ServiceResult Success(string? message = null) => new ServiceResult(true, message);
    public static ServiceResult Failure(string message) => new ServiceResult(false, message);
}

public class ServiceResult<T> : ServiceResult
{
    public T? data { get; private set; }

    private ServiceResult(bool success, T? data = default, string? message = null)
        : base(success, message)
    {
        this.data = data;
    }

    public static ServiceResult<T> Success(T data, string? message = null) => new ServiceResult<T>(true, data, message);
    public static new ServiceResult<T> Failure(string message) => new ServiceResult<T>(false, default, message);
}