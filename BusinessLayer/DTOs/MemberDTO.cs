using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs
{
    public class MemberDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ContactInfo { get; set; }
       
    }
}
