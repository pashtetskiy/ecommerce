namespace Ecommerce.Domain.Shared;

public record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
}

public sealed record ValidationError(Dictionary<string, string[]> ValidationErrors)
    : Error("Validation.Failed", "One or more validation errors occurred.");