using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("Feedback")]
    public class Feedback
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Comment { get; set; }
        public DateTime DateSubmitted { get; set; }

        public virtual Member member { get; set; }
    }
}
