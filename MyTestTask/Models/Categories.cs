using System.ComponentModel.DataAnnotations;
namespace MyTestTask.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Category { get; set; }
        public List<Subcategories> Subcategories { get; set; } = new List<Subcategories>();
    }
}
