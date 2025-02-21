using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("Fine")]
    public class Fine
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime IssuedDate { get; set; }
        [Required]
        public bool IsPaid { get; set; }

        public virtual Member member { get; set; }
    }
}
