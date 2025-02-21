using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("Reservation")]

    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public DateTime ReservationDate { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }

        public virtual Member member { get; set; }
    }
}
