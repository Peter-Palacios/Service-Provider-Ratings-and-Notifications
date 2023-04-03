using Microsoft.AspNetCore.Mvc;
using Service_Provider_Ratings_and_Notifications.Models;
using Service_Provider_Ratings_and_Notifications.Services;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;


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

        private async void NotifyNewRating(Rating rating)
        {
            using HttpClient client = new HttpClient();
            string notificationsUrl = "http://localhost:8080/notifications";

            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                ProviderId = rating.ProviderId,
                Message = $"New rating of {rating.Value} for provider {rating.ProviderId}"
            };

            string json = JsonConvert.SerializeObject(notification);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(notificationsUrl, content);
        }
    }
}
