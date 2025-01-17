namespace DataAccessLayer.DataDTOs
{
    public class BorrowData
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public string Member_Name { get; set; }
        public string Member_ContactInfo { get; set; }
        public DateTime MembershipDate { get; set; }

        public string Book_Title { get; set; }
        public string Book_Author { get; set; }
        public string Book_Genre { get; set; }
        public int Book_PublicationYear { get; set; }
    }
}
