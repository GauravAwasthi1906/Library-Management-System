using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("Member")]
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public DateTime MembershipDate { get; set; }

        public virtual ICollection<Feedback> feedback { get; set;}
        public virtual ICollection<Fine> fine { get; set;}
        public virtual ICollection<Reservation> reservation { get; set;}
        public virtual ICollection<Borrow> borrow { get; set;}
    }
}
