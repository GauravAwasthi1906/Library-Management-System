using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("Borrow")]
    public class Borrow
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public DateTime? ReturnDate { get; set; }
        

        public virtual Member member { get; set; }
        public virtual Book books { get; set; }
    }
}
