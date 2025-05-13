using MediatR;
using SecondHandECommerce.Application.Reviews.DTOs;

namespace SecondHandECommerce.Application.Reviews.Commands.GetReviewsBySeller;

public record GetReviewsBySellerCommand(Guid SellerId) : IRequest<GetReviewsBySellerDto>;