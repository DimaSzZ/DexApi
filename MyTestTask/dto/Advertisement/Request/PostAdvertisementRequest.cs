namespace MyTestTask.dto.AdvertisementFolder.Request
{
    public class PostAdvertisementRequest
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Number { get; set; }
        public string? City { get; set; }
        public string? Category { get; set; }

        public string? Subcategory { get; set; }
        public string? Description { get; set; }
        
    }
}
