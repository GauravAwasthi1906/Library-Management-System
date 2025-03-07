using BusinessLayer.DTOs;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interface
{
    public interface IMemberService
    {
        Task<ServiceResponse> AddMember(MemberDTO member);
        Task<ServiceResponse> UpdateMember(int? id , MemberDTO member);
        Task<ServiceResponse> DeleteMember(int? id);
        Task<MemberData> GetMemberById(int? id);
        Task<List<MemberData>> GetAllMembers();
    }
}
