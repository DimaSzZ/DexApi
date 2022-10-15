using System.ComponentModel.DataAnnotations;

namespace MyTestTask.Models
{

    public class Advertising
    {
        [Key]
        public int Id { get; set; }
        public double Price { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Category { get; set; }

        public string? Subcategory { get; set; }
        public string? Description { get; set; }
        [Required]
        public string? Number { get; set; }
        public DateTimeOffset PublicationDate { get; set; }
        public int PersonId { get; set; }
        public Person? Persona { get; set; }
    }
}
