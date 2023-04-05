namespace Service_Provider_Ratings_and_Notifications.Models
{
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid ProviderId { get; set; }
        public string? Message { get; set; }
    }
}
