using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Services
{
    public class BorrowService : IBorrowService
    {
        public Task<ServiceResponse> AddBorrow(Borrow borrow)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse?> DeleteBorrow(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Borrow>> GetAllBorrows()
        {
            throw new NotImplementedException();
        }

        public Task<Borrow> GetBorrow(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateBorrow(int? id, Borrow borrow)
        {
            throw new NotImplementedException();
        }
    }
}
