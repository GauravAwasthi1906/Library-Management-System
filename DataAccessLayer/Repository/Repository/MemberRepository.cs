using DataAccessLayer.Data;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;
        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewMember(string name, string contactinfo)
        {
            try {
                return await _context.Database.ExecuteSqlRawAsync(
                    "EXEC AddnewMember @p0,@p1",
                    name, contactinfo);
            } catch (Exception ex) {
                throw new Exception($"Error while inserting member data: {ex.Message}", ex);
            }
        }

        public async Task<int> DeleteMember(int id)
        {
            try
            {
                return await _context.Database.ExecuteSqlRawAsync
                    ($"EXEC DeleteMember @p0", id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<MemberData>> GetAllMembers()
        {

            var member = await _context.Database
                .SqlQueryRaw<MemberData>("EXEC GetAllMembers")
                .ToListAsync();

            return member;
        }

        public async Task<MemberData?> GetMemberById(int? id)
        {
            try
            {
                var memberEntity = await _context.Database
                .SqlQueryRaw<MemberData>("EXEC GetMemberById @p0", id)
                .ToListAsync();
                return memberEntity.FirstOrDefault();
            }
            catch (Exception ex) { 
                throw new Exception(ex.Message);
            }
        }

        public async  Task<int> UpdateMember(int id, string name, string contactinfo)
        {
            try
            {
                return await _context.Database.ExecuteSqlRawAsync(
                   "EXEC UpdateMember @p0, @p1 , @p2", id
                   ,name, contactinfo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
