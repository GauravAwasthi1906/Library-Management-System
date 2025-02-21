using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs
{
    public class FeedbackDTO
    {
        [Required]
        public int MemberId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime DateSubmitted { get; set; }
    }
}
