using neighbor_chef.Models;
using neighbor_chef.Models.DTOs.Reviews;
using neighbor_chef.UnitOfWork;

namespace neighbor_chef.Services.Reviews;

public class ReviewsService : IReviewsService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Review> CreateReviewAsync(Guid customerId, CreateReviewDto reviewDto)
    {
        var review = new Review
        {
            CustomerId = customerId,
            ChefId = reviewDto.ChefId,
            Rating = reviewDto.Rating,
            Comment = reviewDto.Comment,
        };

        await _unitOfWork.GetRepository<Review>().AddAsync(review);
        await _unitOfWork.CompleteAsync();

        return review;
    }

    public async Task<Review> GetReviewAsync(Guid id)
    {
        var review = await _unitOfWork.GetRepository<Review>().GetByIdAsync(id);
        if (review == null)
        {
            throw new KeyNotFoundException("Review not found.");
        }

        return review;
    }

    public async Task<IEnumerable<Review>> GetAllReviewsAsync()
    {
        return await _unitOfWork.GetRepository<Review>().GetAllAsync();
    }

    public async Task<IEnumerable<Review>> GetReviewsForChefAsync(Guid chefId)
    {
        var reviews = await _unitOfWork.GetRepository<Review>().GetAllAsync();
        return reviews.Where(r => r.ChefId == chefId);
    }

    public async Task<Review> UpdateReviewAsync(Guid customerId, Guid reviewId, UpdateReviewDto reviewDto)
    {
        var reviewRepository = _unitOfWork.GetRepository<Review>();
        var review = await reviewRepository.GetByIdAsync(reviewId);

        if (review == null)
        {
            throw new KeyNotFoundException("Review not found.");
        }

        if (review.CustomerId != customerId)
        {
            throw new UnauthorizedAccessException("You can only update your own reviews.");
        }

        review.Rating = reviewDto.Rating ?? review.Rating;
        review.Comment = reviewDto.Comment ?? review.Comment;

        await reviewRepository.UpdateAsync(review);
        await _unitOfWork.CompleteAsync();

        return review;
    }

    public async Task DeleteReviewAsync(Guid id, Guid customerId)
    {
        var reviewRepository = _unitOfWork.GetRepository<Review>();
        var review = await reviewRepository.GetByIdAsync(id);

        if (review == null)
        {
            throw new KeyNotFoundException("Review not found.");
        }
        
        var customer = await _unitOfWork.GetRepository<Customer>().GetByIdAsync(customerId);
        if (customer == null)
        {
            throw new KeyNotFoundException("Customer not found.");
        }
        
        if (review.CustomerId != customerId)
        {
            throw new UnauthorizedAccessException("You can only delete your own reviews.");
        }

        await reviewRepository.DeleteAsync(review);
        await _unitOfWork.CompleteAsync();
    }
}