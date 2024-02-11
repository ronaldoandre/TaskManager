namespace TaskManager.MVC.Abstractions;

class ResponseDto<T>(T? data, string? error = null)
{
    public T? Data { get; set; } = data;

    public string? Error { get; set; } = error;
}
