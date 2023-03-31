using Service_Provider_Ratings_and_Notifications.Models;

namespace Service_Provider_Ratings_and_Notifications.Services
{
    public interface IRatingService
    {
        void AddRating(Rating rating);
        double GetAverageRating(Guid providerId);
    }
}
