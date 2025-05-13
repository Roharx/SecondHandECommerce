using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondHandECommerce.Application.Orders.Commands.CreateOrder;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Application.Orders.Commands.UpdateOrderStatus;
using SecondHandECommerce.Application.Orders.DTOs;
using SecondHandECommerce.Domain.Enums;
using SecondHandECommerce.Domain.Helpers;

namespace SecondHandECommerce.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepo;

    public OrdersController(IMediator mediator, IOrderRepository orderRepo)
    {
        _mediator = mediator;
        _orderRepo = orderRepo;
    }

    [Authorize(Roles = "buyer,admin")]
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _orderRepo.GetByIdAsync(id);
        if (order == null) return NotFound();

        return Ok(new OrderDto
        {
            Id = order.Id,
            ListingId = order.ListingId,
            BuyerId = order.BuyerId,
            PriceAtPurchase = order.PriceAtPurchase,
            OrderedAt = order.OrderedAt,
            Status = order.Status.GetDisplayName()
        });
    }

    [HttpGet("by-buyer/{buyerId:guid}")]
    public async Task<IActionResult> GetByBuyer(Guid buyerId)
    {
        var orders = await _orderRepo.GetByBuyerIdAsync(buyerId);

        var results = orders.Select(order => new OrderDto
        {
            Id = order.Id,
            ListingId = order.ListingId,
            BuyerId = order.BuyerId,
            PriceAtPurchase = order.PriceAtPurchase,
            OrderedAt = order.OrderedAt,
            Status = order.Status.GetDisplayName()
        });

        return Ok(results);
    }

    [Authorize(Roles = "seller,admin")]
    [HttpPatch("{orderId:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid orderId, [FromBody] string newStatus)
    {
        if (!Enum.TryParse<OrderStatus>(newStatus, true, out var parsedStatus))
            return BadRequest("Invalid order status.");

        var result = await _mediator.Send(new UpdateOrderStatusCommand
        {
            OrderId = orderId,
            NewStatus = parsedStatus
        });

        return Ok(result);
    }
}