using MediatR;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Application.Orders.DTOs;
using SecondHandECommerce.Domain.Helpers;

namespace SecondHandECommerce.Application.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, OrderDto>
{
    private readonly IOrderRepository _repo;

    public UpdateOrderStatusHandler(IOrderRepository repo)
    {
        _repo = repo;
    }
    public async Task<OrderDto> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _repo.GetByIdAsync(request.OrderId);
        if (order == null)
            throw new Exception("Order not found.");

        if (!OrderStatusRules.CanTransition(order.Status, request.NewStatus))
            throw new Exception($"Cannot transition from {order.Status} to {request.NewStatus}.");

        order.Status = request.NewStatus;
        await _repo.UpdateAsync(order);

        return new OrderDto
        {
            Id = order.Id,
            ListingId = order.ListingId,
            BuyerId = order.BuyerId,
            PriceAtPurchase = order.PriceAtPurchase,
            OrderedAt = order.OrderedAt,
            Status = order.Status.ToString()
        };
    }

}