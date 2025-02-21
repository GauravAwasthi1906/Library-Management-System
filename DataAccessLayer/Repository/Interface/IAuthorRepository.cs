using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repository.Interface
{
    public interface IAuthorRepository
    {
        Task<int> AddnewData(Author author);
        Task<int> UpdateData(int id,string name,string biography);
        Task<List<AuthorData>> GetAllData();
        Task<AuthorData> GetById(int? id);
        Task<int> DeleteData(int? id);
    }
}
