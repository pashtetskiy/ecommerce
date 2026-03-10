using Ecommerce.Api.Extensions;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController(
    IOrderService orderService,
    IValidator<Contracts.CreateOrderDto> validator) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await orderService.GetAllAsync();
        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await orderService.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Contracts.CreateOrderDto dto)
    {
        var validationResult = await validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return HandleFailure(Result.Failure(validationResult.ToValidationError()));
        }

        var result = await orderService.CreateAsync(dto);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value)
            : HandleFailure(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await orderService.DeleteAsync(id);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}