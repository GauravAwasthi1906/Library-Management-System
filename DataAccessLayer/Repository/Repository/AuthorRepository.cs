using DataAccessLayer.Data;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Repository.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddnewData(Author author)
        {
            try
            {
                return await _context.Database.ExecuteSqlRawAsync(
                    "EXEC AddNewAuthor @p0, @p1",
                    author.Name, author.Biography);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while inserting author data: {ex.Message}", ex);
            }
        }

        public async Task<int> DeleteData(int? id)
        {
            try {
                return await _context.Database.ExecuteSqlRawAsync
                    ($"EXEC DeleteAuthor @p0", id);
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AuthorData>> GetAllData()
        {
            try
            {
                var authors = await _context.Database
                .SqlQueryRaw<AuthorData>("EXEC GetAllAuthors")
                .ToListAsync();

                return authors;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AuthorData?> GetById(int? id)
        {
            try
            {
                var authorEntities = await _context.Database
                .SqlQueryRaw<AuthorData>("EXEC GetAuthorById @p0", id)
                .ToListAsync();

                return authorEntities.FirstOrDefault();
            }
            catch (Exception ex) { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateData(int id, string name, string biography)
        {
            try
            {
                return await _context.Database.ExecuteSqlRawAsync(
                   "EXEC UpdateAuthor @p0, @p1 , @p2", id
                   ,name, biography);
            }
            catch( Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
