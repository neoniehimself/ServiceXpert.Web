using System.Net;

namespace ServiceXpert.Web.Models;
public class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; }

    public bool IsSuccess { get; set; }

    public IReadOnlyCollection<string> Errors { get; set; } = [];
}

public class ApiResponse<T> : ApiResponse
{
    public T Value { get; set; } = default!;
}
