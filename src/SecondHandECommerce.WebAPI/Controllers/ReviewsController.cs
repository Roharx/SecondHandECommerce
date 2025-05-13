using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondHandECommerce.Application.Reviews.Commands.CreateReview;
using SecondHandECommerce.Application.Reviews.Commands.GetReviewsBySeller;

namespace SecondHandECommerce.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { ReviewId = id });
        }
        
        [HttpGet("by-seller/{sellerId:guid}")]
        public async Task<IActionResult> GetBySeller(Guid sellerId)
        {
            var reviews = await _mediator.Send(new GetReviewsBySellerCommand(sellerId));
            return Ok(reviews);
        }
    }
}