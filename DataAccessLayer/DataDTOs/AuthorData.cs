using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DataDTOs
{
    public class AuthorData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Biography { get; set; }
    }
}
