using BusinessLayer.DTOs;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interface
{
    public interface IBookService
    {
        Task<ServiceResponse> AddBookData(Book book);
        Task<ServiceResponse> DeleteBookData(int? id);
        Task<ServiceResponse> UpdateBookData(int?id ,Book book);
        Task<List<BookData>> GetAllBookData();
        Task<BookData> GetBookById(int? id);
    }
}
