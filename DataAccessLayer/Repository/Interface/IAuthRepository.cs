using DataAccessLayer.Entities;

namespace DataAccessLayer.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<Employee> GetUserByMail(string email);
    }
}
