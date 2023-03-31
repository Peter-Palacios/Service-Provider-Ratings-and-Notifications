using Microsoft.AspNetCore.Mvc;
using Service_Provider_Ratings_and_Notifications.Models;
using Service_Provider_Ratings_and_Notifications.Services;

namespace Service_Provider_Ratings_and_Notifications.Controllers
{
    [Route("ratings")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService rating_Service;
        public RatingController(IRatingService ratingService)
        {
            rating_Service = ratingService;
        }
        [HttpPost]
        public IActionResult Post([FromBody] Rating rating)
        {
            rating_Service.AddRating(rating);
            NotifyNewRating(rating);
            return Ok();
        }
        [HttpGet("{providerId}")]
        public IActionResult Get(Guid providerId)
        {
            double averageRating = rating_Service.GetAverageRating(providerId);
            return Ok(averageRating);
        }

        private void NotifyNewRating(Rating rating)
        {

        }
    }
}
