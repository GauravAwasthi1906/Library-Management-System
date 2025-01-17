namespace DataAccessLayer.DataDTOs
{
    public class FeedbackData
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberContactInfo { get; set; }
        public DateTime MembershipDate { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string Comment { get; set; }
    }
}
