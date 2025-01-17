using BusinessLayer.DTOs;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interface
{
    public interface IMemberService
    {
        Task<ServiceResponse> AddMember(Member member);
        Task<ServiceResponse> UpdateMember(int? id ,Member member);
        Task<ServiceResponse> DeleteMember(int? id);
        Task<MemberData> GetMemberById(int? id);
        Task<List<MemberData>> GetAllMembers();
    }
}
