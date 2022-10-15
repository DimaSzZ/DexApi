namespace MyTestTask.dto.AdvertisementFolder.Response
{
    public class GetAdvertisementResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Number { get; set; }
        public double Price { get; set; }
   
        public string? City { get; set; }

        public string? Category { get; set; }

        public DateTimeOffset PublicationDate { get; set; } 
        public string? Subcategory { get; set; }

    }
}
