using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository.Interface;

namespace DataAccessLayer.Repository.Repository
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly AppDbContext _context;
        public BorrowRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Borrow>> GetAllBorrow()
        {
            throw new NotImplementedException();
        }

        public Task<Borrow> GetBorrow(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
