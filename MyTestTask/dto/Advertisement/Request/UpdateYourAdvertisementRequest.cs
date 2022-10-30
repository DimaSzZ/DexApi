namespace MyTestTask.dto.Advertisement.Request
{
    public class UpdateYourAdvertisementRequest
    {
        public Guid Id { get; set; }

        public string Number { get; set; }
        public string? Description { get; set; }
        public string Page { get; set; }
        public string Rating { get; set; }
    }
}
