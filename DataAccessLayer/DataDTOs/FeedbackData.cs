namespace DataAccessLayer.DataDTOs
{
    public class FeedbackData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string ContactInfo { get; set; }
        public DateTime MembershipDate { get; set; }
    }
}
