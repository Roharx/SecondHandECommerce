using MediatR;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Domain.Entities;

namespace SecondHandECommerce.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, Guid>
    {
        private readonly IReviewRepository _repo;

        public CreateReviewHandler(IReviewRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = new Review
            {
                Id = Guid.NewGuid(),
                BuyerId = request.BuyerId,
                SellerId = request.SellerId,
                Rating = request.Rating,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.CreateAsync(review);
            return review.Id;
        }
    }
}