using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repository.Interface
{
    public interface IAuthorRepository
    {
        Task<int> AddnewData(Author author);
        Task<List<AuthorData>> GetAllData();
        Task<AuthorData> GetById(int? id);
    }
}
