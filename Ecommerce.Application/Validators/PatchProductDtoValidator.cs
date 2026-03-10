using Ecommerce.Application.DTOs;
using FluentValidation;

namespace Ecommerce.Application.Validators;

public class PatchProductDtoValidator : AbstractValidator<Contracts.PatchProductDto>
{
    public PatchProductDtoValidator()
    {
        RuleFor(x => x.Version)
            .NotEmpty().WithMessage("Version is required");

        When(x => x.Name is not null, () =>
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(100);
        });

        When(x => x.Price is not null, () =>
        {
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        });
    }
}