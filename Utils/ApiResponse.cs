namespace DepartementService.Utils;

public class ApiResponse // Generic response wrapper
{
    public object? Data { get; set; }
    public Dictionary<string, object>? ViewBag { get; set; }
    public string? Message { get; set; }
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    // Add any other properties you need (e.g., errors, metadata)
}