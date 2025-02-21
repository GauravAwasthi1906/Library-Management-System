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
                    "EXEC AddAuthorData @p0, @p1",
                    author.Name, author.Biography);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while inserting author data: {ex.Message}", ex);
            }
        }


        public async Task<List<AuthorData>> GetAllData()
        {
            try
            {
                var authorEntities = await _context.author
                .FromSqlRaw("EXEC GetAuthorData")
                .AsNoTracking()
                .ToListAsync();

                var authors = authorEntities.Select(a => new AuthorData
                {
                    Id = a.Id,
                    Name = a.Name,
                    Biography = a.Biography,
                }).ToList();
                return authors;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AuthorData> GetById(int? id)
        {
            try
            {
                var authorEntities = _context.author
         .FromSqlRaw("EXEC GETAUTHORDATABYID {0}", id)
         .AsEnumerable() 
         .FirstOrDefault();
                return authorEntities == null ? null : new AuthorData
                {
                    Id = authorEntities.Id,
                    Name = authorEntities.Name,
                    Biography = authorEntities.Biography,
                };
            }
            catch (Exception ex) { 
                throw new Exception(ex.Message);
            }
        }
    }
}
