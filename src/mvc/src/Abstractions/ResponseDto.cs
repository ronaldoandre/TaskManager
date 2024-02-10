namespace TaskManager.MVC.Abstractions;

class ResponseDto(object? data, string? error = null)
{
    public object? Data { get; set; } = data;

    public string? Error { get; set; } = error;
}
