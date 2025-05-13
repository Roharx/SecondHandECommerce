using MediatR;

namespace SecondHandECommerce.Application.Reviews.Commands.CreateReview
{
    public record CreateReviewCommand(Guid BuyerId, Guid SellerId, int Rating, string? Comment) : IRequest<Guid>;
}