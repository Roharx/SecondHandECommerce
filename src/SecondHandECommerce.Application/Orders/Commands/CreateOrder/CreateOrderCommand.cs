using MediatR;
using SecondHandECommerce.Application.Orders.DTOs;

namespace SecondHandECommerce.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<OrderDto>
{
    public Guid ListingId { get; set; }
    public Guid BuyerId { get; set; }
}