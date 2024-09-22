namespace Application.Bases.Dtos.Paginations;

public enum FilterOperator
{
    Contains = 0,
    EqualCaseSensitive = 1,
    EqualCaseInsensitive = 2,
    NotEqual = 3,
    LessThanOrEqual = 4,
    LessThan = 5,
    GreaterThanOrEqual = 6,
    GreaterThan = 7,
    IsNull = 8,
    NotNull = 9
}
