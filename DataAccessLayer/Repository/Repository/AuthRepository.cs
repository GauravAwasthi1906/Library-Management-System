using System.Linq.Expressions;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetUserByMail(string email)
        {
            try { 
                var data = from i in _context.employee 
                           where i.Email == email 
                           select i;
                var employee = await data.FirstOrDefaultAsync();
                return employee;
            }catch (Exception ex) {
                throw new Exception(ex.Message,ex);
            }
        }
    }
}
