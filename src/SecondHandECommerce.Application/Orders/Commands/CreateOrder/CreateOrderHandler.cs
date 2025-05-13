using MediatR;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Application.Orders.DTOs;
using SecondHandECommerce.Domain.Entities;
using SecondHandECommerce.Domain.Enums;
using SecondHandECommerce.Domain.Helpers;

namespace SecondHandECommerce.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _orderRepo;
    private readonly IListingRepository _listingRepo;
    private readonly IUserRepository _userRepo;

    public CreateOrderHandler(IOrderRepository orderRepo, IListingRepository listingRepo, IUserRepository userRepo)
    {
        _orderRepo = orderRepo;
        _listingRepo = listingRepo;
        _userRepo = userRepo;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var listing = await _listingRepo.GetByIdAsync(request.ListingId);
        if (listing == null)
            throw new Exception("Listing does not exist.");

        var buyer = await _userRepo.GetByIdAsync(request.BuyerId);
        if (buyer == null)
            throw new Exception("Invalid buyer ID.");

        var order = new Order
        {
            Id = Guid.NewGuid(),
            ListingId = request.ListingId,
            BuyerId = request.BuyerId,
            PriceAtPurchase = listing.Price,
            OrderedAt = DateTime.UtcNow,
            Status = OrderStatus.Pending

        };

        await _orderRepo.CreateAsync(order);

        return new OrderDto
        {
            Id = order.Id,
            ListingId = order.ListingId,
            BuyerId = order.BuyerId,
            PriceAtPurchase = order.PriceAtPurchase,
            OrderedAt = order.OrderedAt,
            Status = order.Status.GetDisplayName()
        };
    }
}