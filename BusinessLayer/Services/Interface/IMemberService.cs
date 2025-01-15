using BusinessLayer.DTOs;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interface
{
    public interface IMemberService
    {
        Task<ServiceResponse> AddMember(Member member);
        Task<ServiceResponse> UpdateMember(int? id ,Member member);
        Task<ServiceResponse> DeleteMember(int? id);
        Task<Member> GetMemberById(int? id);
        Task<List<Member>> GetAllMembers();
    }
}
