using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs
{
    public class BorrowDTO
    {
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
    }
}
