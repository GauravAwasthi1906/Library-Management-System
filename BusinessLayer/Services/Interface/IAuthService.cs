using BusinessLayer.DTOs;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interface
{
    public interface IAuthService
    {
        Task<ServiceResponse> SignIn(Employee employee);

        Task<ServiceResponse>SignUp(Employee employee);
    }
}
