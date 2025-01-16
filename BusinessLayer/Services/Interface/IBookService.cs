using BusinessLayer.DTOs;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interface
{
    public interface IBookService
    {
        Task<ServiceResponse> AddBookData(Book book);
        Task<ServiceResponse> DeleteBookData(int? id);
        Task<ServiceResponse> UpdateBookData(int?id ,Book book);
        Task<List<Book>> GetAllBookData();
        Task<Book> GetBookById(int? id);
    }
}
