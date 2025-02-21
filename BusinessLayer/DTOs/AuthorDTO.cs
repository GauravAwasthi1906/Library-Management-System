using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs
{
    public class AuthorDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Biography { get; set; }
    }
}
