using Ecommerce.Api.Extensions;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Route("api/products")]
public class ProductsController(
    IProductService productService,
    IValidator<Contracts.CreateProductDto> createValidator,
    IValidator<Contracts.PatchProductDto> patchValidator) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await productService.GetAllAsync();
        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await productService.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Contracts.CreateProductDto dto)
    {
        var validationResult = await createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            return HandleFailure(Result.Failure(validationResult.ToValidationError()));

        var result = await productService.CreateAsync(dto);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value)
            : HandleFailure(result);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(Guid id, Contracts.PatchProductDto dto)
    {
        var validationResult = await patchValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            return HandleFailure(Result.Failure(validationResult.ToValidationError()));

        var result = await productService.PatchAsync(id, dto);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await productService.DeleteAsync(id);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}