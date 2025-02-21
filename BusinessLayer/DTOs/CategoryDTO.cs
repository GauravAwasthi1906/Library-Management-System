using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs
{
    public class CategoryDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
