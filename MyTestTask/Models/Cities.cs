using System.ComponentModel.DataAnnotations;
namespace MyTestTask.Models
{
    public class Cities
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? City { get; set; }
    }
}
