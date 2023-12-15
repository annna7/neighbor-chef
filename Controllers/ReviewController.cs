using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using neighbor_chef.Models.DTOs.Reviews;
using neighbor_chef.Services.Reviews;
using neighbor_chef.Services;

namespace neighbor_chef.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewsService _reviewsService;
        private readonly ICustomerService _customerService;

        public ReviewController(IReviewsService reviewsService, ICustomerService customerService)
        {
            _reviewsService = reviewsService;
            _customerService = customerService;
        }

        [HttpPost]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto createReviewDto)
        {
            try
            {
                var customerEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (customerEmail == null) return Unauthorized("You are not authorized to create a review, please log in as a customer.");
                var customer = await _customerService.GetPersonAsync(customerEmail);
                if (customer == null) return Unauthorized("You are not authorized to create a review, please log in as a customer.");
                var review = await _reviewsService.CreateReviewAsync(customer.Id, createReviewDto);
                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetReview(Guid id)
        {
            try
            {
                var review = await _reviewsService.GetReviewAsync(id);
                return Ok(review);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Review not found.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewsService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("chef/{chefId:guid}")]
        public async Task<IActionResult> GetReviewsForChef(Guid chefId)
        {
            var reviews = await _reviewsService.GetReviewsForChefAsync(chefId);
            return Ok(reviews);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateReview(Guid id, [FromBody] UpdateReviewDto updateReviewDto)
        {
            try
            {
                var customerEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (customerEmail == null) return Unauthorized("Please log in as a customer.");
                var customer = await _customerService.GetPersonAsync(customerEmail);
                if (customer == null) return Unauthorized("You can only update your own reviews.");
                var review = await _reviewsService.UpdateReviewAsync(customer.Id, id, updateReviewDto);
                return Ok(review);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Review not found.");
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("You can only update your own reviews.");
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Customer", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            try
            {
                var customerEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (customerEmail == null) return Unauthorized("You are not authorized to create a review, please log in as a customer.");
                var customer = await _customerService.GetPersonAsync(customerEmail);
                if (customer == null) return Unauthorized("You are not authorized to create a review, please log in as a customer.");
                await _reviewsService.DeleteReviewAsync(id, customer.Id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Review not found.");
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("You can only delete your own reviews.");
            }
        }
    }
}
