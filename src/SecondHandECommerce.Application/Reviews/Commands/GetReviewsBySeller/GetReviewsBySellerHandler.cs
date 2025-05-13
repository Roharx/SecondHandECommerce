using MediatR;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Application.Reviews.DTOs;

namespace SecondHandECommerce.Application.Reviews.Commands.GetReviewsBySeller;

public class GetReviewsBySellerHandler : IRequestHandler<GetReviewsBySellerCommand, GetReviewsBySellerDto>
{
    private readonly IReviewRepository _repo;

    public GetReviewsBySellerHandler(IReviewRepository repo)
    {
        _repo = repo;
    }

    public async Task<GetReviewsBySellerDto> Handle(GetReviewsBySellerCommand request, CancellationToken cancellationToken)
    {
        var reviews = await _repo.GetBySellerIdAsync(request.SellerId);

        float average = reviews.Count > 0 ? (float)reviews.Average(r => r.Rating) : 0;

        return new GetReviewsBySellerDto
        {
            AverageRating = average,
            Reviews = reviews
        };
    }
}