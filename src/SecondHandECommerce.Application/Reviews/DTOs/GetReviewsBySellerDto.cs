using SecondHandECommerce.Domain.Entities;
using System.Collections.Generic;

namespace SecondHandECommerce.Application.Reviews.DTOs;

public class GetReviewsBySellerDto
{
    public float AverageRating { get; set; }
    public List<Review> Reviews { get; set; } = new();
}