using System.ComponentModel.DataAnnotations;

namespace MyTestTask.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Number { get; set; }
        public List<Advertising> Advertising { get; set; } = new List<Advertising>();
    }
}
