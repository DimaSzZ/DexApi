using System.ComponentModel.DataAnnotations;

namespace MyTestTask.Models
{
    ///<summary>
    ///Модель б\д
    ///</summary>
    public class Person
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public bool Admin { get; set; }
        public List<Ad> Advertising { get; set; } = new List<Ad>();
    }
}
