using DataAccessLayer.Data;
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

        public async Task<List<Borrow>> GetAllBorrow()
        {
            var data = from i in _context.borrow
                       join j in _context.member on i.MemberId equals j.Id
                       join k in _context.book on i.BookId equals k.Id
                       select new Borrow
                       {
                           Id = i.Id,
                           MemberId = i.MemberId,
                           BookId = i.BookId,
                           BorrowDate = i.BorrowDate,
                           DueDate = i.DueDate,
                           ReturnDate = i.ReturnDate,
                           member = new Member
                           {
                               Id = j.Id,
                               Name = j.Name,
                               ContactInfo = j.ContactInfo,
                               MembershipDate = j.MembershipDate,
                           },
                           books = new Book
                           {
                               Id = k.Id,
                               Title = k.Title,
                               Author = k.Author,
                               Genre = k.Genre,
                               PublicationYear = k.PublicationYear
                           }
                       };
            return await data.ToListAsync();

        }

        public async Task<Borrow> GetBorrow(int? id)
        {
            var data = from i in _context.borrow
                       join j in _context.member on i.MemberId equals j.Id
                       join k in _context.book on i.BookId equals k.Id
                       where i.Id == id
                       select new Borrow
                       {
                           Id = i.Id,
                           MemberId = i.MemberId,
                           BookId = i.BookId,
                           BorrowDate = i.BorrowDate,
                           DueDate = i.DueDate,
                           ReturnDate = i.ReturnDate,
                           member = new Member
                           {
                               Id = j.Id,
                               Name = j.Name,
                               ContactInfo = j.ContactInfo,
                               MembershipDate = j.MembershipDate,
                           },
                           books = new Book
                           {
                               Id = k.Id,
                               Title = k.Title,
                               Author = k.Author,
                               Genre = k.Genre,
                               PublicationYear = k.PublicationYear
                           }
                       };
            return await data.FirstAsync();
        }
    }
}
