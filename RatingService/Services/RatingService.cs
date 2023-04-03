using Service_Provider_Ratings_and_Notifications.Models;

namespace Service_Provider_Ratings_and_Notifications.Services
{
    public class RatingServiceImp:IRatingService
    {
        //private List<Rating> _ratings = new List<Rating>();

        private readonly List<Rating> _ratings = new();

        public void AddRating(Rating rating)
        {
            _ratings.Add(rating);
        }

        public double GetAverageRating(Guid providerId)
        {
            var rating = _ratings.Where(r => r.ProviderId == providerId).ToList();
            if (rating.Count == 0) return 0;
            return rating.Average(r => r.Value);
        }

        public RatingServiceImp()
        {
            // Seed data
            _ratings.Add(new Rating { ProviderId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Value = 5 });
            _ratings.Add(new Rating { ProviderId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Value = 4 });
            _ratings.Add(new Rating { ProviderId = Guid.Parse("22222222-2222-2222-2222-222222222222"), Value = 3 });
            _ratings.Add(new Rating { ProviderId = Guid.Parse("22222222-2222-2222-2222-222222222222"), Value = 5 });
        }

    }
}
