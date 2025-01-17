using DataAccessLayer.DataDTOs;

namespace DataAccessLayer.Repository.Interface
{
    public interface IBookRepository
    {
        Task<List<BookData>> GetAllData();
        Task<BookData> GetById(int id);
    }
}
