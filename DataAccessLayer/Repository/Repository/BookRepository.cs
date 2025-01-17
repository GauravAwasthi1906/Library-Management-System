using DataAccessLayer.Data;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<BookData>> GetAllData()
        {
            var data = from i in _context.book
                       select new BookData
                       {
                            Id= i.Id,
                            Title= i.Title,
                            Author= i.Author,
                            Genre= i.Genre,
                           PublicationYear= i.PublicationYear,
                       };
            return await data.ToListAsync();
        }

        public async Task<BookData> GetById(int id)
        {
            var data = from i in _context.book
                       select new BookData
                       {
                           Id = i.Id,
                           Title = i.Title,
                           Author = i.Author,
                           Genre = i.Genre,
                           PublicationYear = i.PublicationYear,
                       };
            return await data.FirstAsync();
        }
    }
}
