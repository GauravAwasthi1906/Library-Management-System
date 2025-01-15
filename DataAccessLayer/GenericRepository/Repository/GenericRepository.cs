using DataAccessLayer.Data;
using DataAccessLayer.GenericRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.GenericRepository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T> AddNewData(T entity)
        {
            try { 
                _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }   catch
            {
                throw new Exception("This Data can not be added");
            }
        }

        public async Task DeleteData(T entity)
        {
            try {
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                throw new Exception("This Data can not be Deleted");
            }
        }

        public async Task<List<T>> GetAllData()
        {
            try { 
                return await _dbSet.ToListAsync();
            }
            catch
            {
                throw new Exception("You can not fetch the Data");
            }
        }

        public async Task<T> GetDataById(int? id)
        {
            
            try {
                return await _dbSet.FindAsync(id);
            }
            catch
            {
                throw new Exception("You can not fetch this Data");
            }
        }

        public async Task<T> UpdateDate(T entity)
        {
            
            try {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw new Exception("This Data can not be updated");
            }
        }
    }
}
