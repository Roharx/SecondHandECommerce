using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondHandECommerce.Application.Interfaces;
using SecondHandECommerce.Application.Listings.Commands.CreateListings;
using SecondHandECommerce.Application.Listings.Commands.SearchListing;
using SecondHandECommerce.Application.Listings.Queries.GetListings;
using SecondHandECommerce.Application.Listings.Queries.GetListings.GetAllListings;
using SecondHandECommerce.Application.Listings.Queries.GetListings.GetListingsBySeller;

namespace SecondHandECommerce.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICloudStorageService _cloudStorage;

    public ListingsController(IMediator mediator, ICloudStorageService cloudStorage)
    {
        _mediator = mediator;
        _cloudStorage = cloudStorage;
    }

    [Authorize(Roles = "seller,admin")]
    [HttpPost]
    public async Task<IActionResult> CreateListing(CreateListingCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { ListingId = id });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetListingById(Guid id)
    {
        var result = await _mediator.Send(new GetListingByIdQuery(id));
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllListings()
    {
        var listings = await _mediator.Send(new GetAllListingsQuery());
        return Ok(listings);
    }

    [HttpGet("by-seller/{sellerId:guid}")]
    public async Task<IActionResult> GetBySeller(Guid sellerId)
    {
        var result = await _mediator.Send(new GetListingsBySellerQuery(sellerId));
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? keyword, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
    {
        var result = await _mediator.Send(new SearchListingsQuery(keyword, minPrice, maxPrice));
        return Ok(result);
    }

    [Authorize(Roles = "seller,admin")]
    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is missing or empty.");

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        using var stream = file.OpenReadStream();
        await _cloudStorage.UploadFileAsync(stream, fileName, file.ContentType);

        var preSignedUrl = await _cloudStorage.GetPreSignedUrlAsync(fileName, TimeSpan.FromHours(1));
        return Ok(new { imageUrl = preSignedUrl });
    }
}
