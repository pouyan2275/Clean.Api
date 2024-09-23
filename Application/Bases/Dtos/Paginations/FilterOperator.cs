using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Application.Bases.Dtos.Paginations;

public enum FilterOperator
{
    [JsonPropertyName("%")]
    Contains = 0,
    [JsonPropertyName("==")]
    Equal = 1,
    [JsonPropertyName("!=")]
    NotEqual = 2,
    [JsonPropertyName("<=")]
    LessThanOrEqual = 3,
    [JsonPropertyName("<")]
    LessThan = 4,
    [JsonPropertyName(">=")]
    GreaterThanOrEqual = 5,
    [JsonPropertyName(">")]
    GreaterThan = 6,
    [JsonPropertyName("IS NULL")]
    IsNull = 7,
    [JsonPropertyName("IS NOT NULL")]
    NotNull = 8
}
