using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repository.Interface
{
    public interface IBorrowRepository
    {
        Task<List<BorrowData>> GetAllBorrow();
        Task<BorrowData> GetBorrow(int? id);
    }
}
