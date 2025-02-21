using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs
{
    public class BookDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public int PublicationYear { get; set; }
    }
}
