using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("Member")]
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ContactInfo { get; set; }
        [Required]
        public DateTime MembershipDate { get; set; }

        public virtual ICollection<Feedback> feedback { get; set;}
        public virtual ICollection<Fine> fine { get; set;}
        public virtual ICollection<Reservation> reservation { get; set;}
        public virtual ICollection<Borrow> borrow { get; set;}
    }
}
