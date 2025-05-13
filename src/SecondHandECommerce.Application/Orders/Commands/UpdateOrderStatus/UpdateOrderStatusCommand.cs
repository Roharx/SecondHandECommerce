using MediatR;
using SecondHandECommerce.Application.Orders.DTOs;
using SecondHandECommerce.Domain.Enums;

namespace SecondHandECommerce.Application.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommand : IRequest<OrderDto>
{
    public Guid OrderId { get; set; }
    public OrderStatus NewStatus { get; set; }
}