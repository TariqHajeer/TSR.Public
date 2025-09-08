using System;

namespace Application.Dtos.Common;

public class HttpResult
{
    public bool IsSuccessStatusCode { get; set; }
    public int StatusCode { get; set; }
    public string Content { get; set; } = string.Empty;

}
