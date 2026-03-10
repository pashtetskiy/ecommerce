using Ecommerce.Application.DTOs;
using FluentValidation;

namespace Ecommerce.Application.Validators;

public class ProductDtoValidator : AbstractValidator<Contracts.CreateProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than 0");
    }
}