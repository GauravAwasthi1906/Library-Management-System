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
                    ($"EXEC DeleteMemberData @p0", id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<MemberData>> GetAllMembers()
        {
            var memberEntity = await _context.member
                .FromSqlRaw("EXEC GetAllMembers;")
                .AsNoTracking()
                .ToListAsync();
            var member = memberEntity.Select(a=> new  MemberData
            {
                Id = a.Id,
                Name = a.Name,
                ContactInfo = a.ContactInfo,
                MembershipDate=a.MembershipDate,
            }).ToList();
            return member;
        }

        public async Task<MemberData?> GetMemberById(int? id)
        {
            try
            {
                var memberEntity =_context.member
                .FromSqlRaw("EXEC GetMemberById @p0", id)
                .AsEnumerable()
                .FirstOrDefault();
                return memberEntity == null ? null : new MemberData
                {
                Id = memberEntity.Id,
                Name = memberEntity.Name,
                ContactInfo = memberEntity.ContactInfo,
                MembershipDate = memberEntity.MembershipDate,
                };
            }
            catch (Exception ex) { 
                throw new Exception(ex.Message);
            }
        }

        public Task<int> UpdateMember(int id, string name, string contactinfo)
        {
            throw new NotImplementedException();
        }
    }
}
