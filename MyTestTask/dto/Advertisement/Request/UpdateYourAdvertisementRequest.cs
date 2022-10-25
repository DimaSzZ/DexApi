namespace MyTestTask.dto.AdvertisementFolder.Request
{
    public class UpdateYourAdvertisementRequest
    {
        public int Id { get; set; }

        public string? NewName { get; set; }
        public double NewPrice { get; set; }
        public string? NewNumber { get; set; }
        public string? NewCity { get; set; }
        public string? NewCategory { get; set; }

        public string? NewSubcategory { get; set; }
        public string? NewDescription { get; set; }
    }
}
