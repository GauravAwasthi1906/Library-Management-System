using DataAccessLayer.Entities;

namespace DataAccessLayer.Repository.Interface
{
    public interface IBorrowRepository
    {
        Task<List<Borrow>> GetAllBorrow();
        Task<Borrow> GetBorrow(int? id);
    }
}
