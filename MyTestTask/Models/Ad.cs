using System.ComponentModel.DataAnnotations;

namespace MyTestTask.Models
{

    public class Ad
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Не указан номер")]
        public string Number { get; set; }
        public string? Description { get; set; }
        public string Page { get; set; }
        [Required(ErrorMessage = "Не указан рейтинг")]
        public string Rating { get; set; }
        [Required]
        public DateTimeOffset PublicationDate { get; set; }
        [Required]
        public DateTimeOffset ExpirationDate { get; set; }
        public Guid PersonId { get; set; }
        public Person? Persons { get; set; }
    }
}
