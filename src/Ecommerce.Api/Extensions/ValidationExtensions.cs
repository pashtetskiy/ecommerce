using Ecommerce.Domain.Shared;
using FluentValidation.Results;

namespace Ecommerce.Api.Extensions;

public static class ValidationExtensions
{
    public static ValidationError ToValidationError(this ValidationResult validationResult)
    {
        var errors = validationResult.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            );

        return new ValidationError(errors);
    }
}
