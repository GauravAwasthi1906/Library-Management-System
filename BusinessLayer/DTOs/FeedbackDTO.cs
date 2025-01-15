namespace BusinessLayer.DTOs
{
    public class FeedbackDTO
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Comment { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}
