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
        public async Task<List<MemberData>> GetAllMembers()
        {
            var data = from i in _context.member
                       select new MemberData
                       {
                           Id= i.Id,
                           Name= i.Name,
                           ContactInfo= i.ContactInfo,
                           MembershipDate= i.MembershipDate,
                       };
            return await data.ToListAsync();
        }

        public async Task<MemberData> GetMemberById(int? id)
        {
            var data = from i in _context.member
                       where i.Id == id
                       select new MemberData
                       {
                           Id = i.Id,
                           Name = i.Name,
                           ContactInfo = i.ContactInfo,
                           MembershipDate = i.MembershipDate,
                       };
            var member=  await data.FirstOrDefaultAsync();
            return member;
        }
    }
}
