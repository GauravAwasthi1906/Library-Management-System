using DataAccessLayer.DataDTOs;

namespace DataAccessLayer.Repository.Interface
{
    public interface IMemberRepository
    {
        Task<List<MemberData>> GetAllMembers();
        Task<MemberData> GetMemberById(int? id);
    }
}
