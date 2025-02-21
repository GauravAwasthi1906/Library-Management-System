using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public int PublicationYear { get; set; }

        public virtual ICollection<Borrow> borrow { get; set; }
    }

}
