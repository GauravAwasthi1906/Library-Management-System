using BusinessLayer.DTOs;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interface
{
    public interface IBorrowService
    {
        Task<ServiceResponse> AddBorrow(Borrow borrow);
        Task<ServiceResponse> UpdateBorrow(int? id,Borrow borrow);
        Task<ServiceResponse?> DeleteBorrow(int? id);
        Task<List<Borrow>> GetAllBorrows();
        Task<Borrow> GetBorrowById(int? id);
    }
}
