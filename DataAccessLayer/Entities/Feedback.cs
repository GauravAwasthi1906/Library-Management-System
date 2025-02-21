using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("Feedback")]
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime DateSubmitted { get; set; }


        public virtual Member member { get; set; }
    }
}
