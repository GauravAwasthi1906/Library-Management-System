using DataAccessLayer.Data;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Repository.Repository
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly AppDbContext _context;
        public BorrowRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BorrowData>> GetAllBorrow()
        {
            var data = from i in _context.borrow
                       join j in _context.member on i.MemberId equals j.Id
                       join k in _context.book on i.BookId equals k.Id
                       select new BorrowData
                       {
                           Id = i.Id,
                           MemberId = i.MemberId,
                           Member_Name = j.Name,
                           Member_ContactInfo = j.ContactInfo,
                           MembershipDate = j.MembershipDate,
                           BorrowDate = i.BorrowDate,
                           DueDate = i.DueDate,
                           ReturnDate = i.ReturnDate,
                           BookId = i.BookId,
                           Book_Title = k.Title,
                           Book_Author=k.Author,
                           Book_Genre=k.Genre,
                           Book_PublicationYear=k.PublicationYear,
                       };
            return await data.ToListAsync();

        }

        public async Task<BorrowData> GetBorrow(int? id)
        {
            var data = from i in _context.borrow
                       join j in _context.member on i.MemberId equals j.Id
                       join k in _context.book on i.BookId equals k.Id
                       where i.Id == id
                       select new BorrowData
                       {
                           Id = i.Id,
                           MemberId = i.MemberId,
                           Member_Name = j.Name,
                           Member_ContactInfo = j.ContactInfo,
                           MembershipDate = j.MembershipDate,
                           BorrowDate = i.BorrowDate,
                           DueDate = i.DueDate,
                           ReturnDate = i.ReturnDate,
                           BookId = i.BookId,
                           Book_Title = k.Title,
                           Book_Author = k.Author,
                           Book_Genre = k.Genre,
                           Book_PublicationYear = k.PublicationYear,
                       };
            return await data.FirstAsync();
        }
    }
}
