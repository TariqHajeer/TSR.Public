using System.Text.Json.Serialization;

namespace Application.Dtos.Common;

public class ResultMessageDto
{
    public bool IsSuccess { get; set; } = true;
    public long Result { get; set; } = 1;
    public List<string> Message { get; set; } = new List<string>();

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ErrorCode { get; set; }

    public string? Claim { get; set; }
}
