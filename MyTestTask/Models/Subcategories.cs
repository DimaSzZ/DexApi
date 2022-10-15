using System.ComponentModel.DataAnnotations;

namespace MyTestTask.Models
{
    public class Subcategories
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Subcategory { get; set; }
        public int? CategoryId { get; set; }
        public Categories? Categories { get; set; }
    }
}
