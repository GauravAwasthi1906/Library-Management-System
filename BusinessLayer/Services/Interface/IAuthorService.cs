using BusinessLayer.DTOs;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interface
{
    public interface IAuthorService
    {
        Task<ServiceResponse> AddAuthorData(AuthorDTO author);
        Task<ServiceResponse> UpdateAuthorData(int? id, AuthorDTO author);
        Task<ServiceResponse> DeleteAuthorData(int? id);
        Task<List<AuthorData>> GetAllAuthorData();
        Task<AuthorData> GetAuthorById(int? id);
    }
}
