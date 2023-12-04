using neighbor_chef.Models;
using neighbor_chef.Models.DTOs.Reviews;

namespace neighbor_chef.Services.Reviews;

public interface IReviewsService
{
    Task<Review> CreateReviewAsync(Guid customerId, CreateReviewDto review);
    Task<Review> GetReviewAsync(Guid id);
    Task<IEnumerable<Review>> GetAllReviewsAsync();
    Task<IEnumerable<Review>> GetReviewsForChefAsync(Guid chefId);
    Task<Review> UpdateReviewAsync(Guid customerId, Guid reviewId, UpdateReviewDto review);
    Task DeleteReviewAsync(Guid id, Guid customerId);
}