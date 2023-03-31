using Service_Provider_Ratings_and_Notifications.Models;

namespace Service_Provider_Ratings_and_Notifications.Services
{
    public class RatingServiceImp:IRatingService
    {
        private readonly List<Rating> ratings = new();

        public void AddRating(Rating rating)
        {
            ratings.Add(rating);
        }

        public double GetAverageRating(Guid providerId)
        {
            var rating = ratings.Where(r => r.ProviderId == providerId).ToList();
            if (rating.Count == 0) return 0;
            return rating.Average(r => r.Value);
        }
    }
}
