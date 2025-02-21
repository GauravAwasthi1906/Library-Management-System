using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Full_Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Designation {  get; set; }

    }
}
