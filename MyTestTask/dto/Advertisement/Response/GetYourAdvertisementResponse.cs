namespace MyTestTask.dto.Advertisement.Response
{
    public class GetYourAdvertisementResponse
    {
        public string Number { get; set; }
        public string? Description { get; set; }
        public string Page { get; set; }
        public string Rating { get; set; }
        public DateTimeOffset PublicationDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
    }
}
