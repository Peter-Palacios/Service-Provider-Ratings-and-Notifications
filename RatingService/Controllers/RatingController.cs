using Microsoft.AspNetCore.Mvc;
using Service_Provider_Ratings_and_Notifications.Models;
using Service_Provider_Ratings_and_Notifications.Services;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;


namespace Service_Provider_Ratings_and_Notifications.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService rating_Service;
        private readonly ILogger<RatingController> logger;
        public RatingController(IRatingService ratingService,ILogger<RatingController> logger)
        {
            rating_Service = ratingService;
            this.logger=logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Rating rating)
        {
            rating_Service.AddRating(rating);
            await NotifyNewRating(rating);
            return Ok();
        }
        [HttpGet("{providerId}")]
        public IActionResult Get(Guid providerId)
        {
            double averageRating = rating_Service.GetAverageRating(providerId);
            return Ok(averageRating);
        }


        private async Task NotifyNewRating(Rating rating)
{
    try
    {
        using HttpClient client = new HttpClient();
        string notificationsUrl = "http://notification_service:8080/notifications";

        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            ProviderId = rating.ProviderId,
            Message = $"New rating of {rating.Value} for provider {rating.ProviderId}"
        };

        string json = JsonConvert.SerializeObject(notification);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(notificationsUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            // Log the error or throw an exception
             logger.LogError($"Error sending notification: {response.StatusCode}");
        }
    }
    catch (Exception ex)
    {
        // Log the exception
        logger.LogError($"Error sending notification: {ex.Message}");
    }
}

    }
}
