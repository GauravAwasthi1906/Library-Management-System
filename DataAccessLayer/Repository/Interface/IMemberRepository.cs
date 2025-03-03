using DataAccessLayer.DataDTOs;

namespace DataAccessLayer.Repository.Interface
{
    public interface IMemberRepository
    {
        Task<int> AddNewMember(string name, string contactinfo);
        Task<int> UpdateMember(int id,string name, string contactinfo);
        Task<int> DeleteMember(int id);
        Task<List<MemberData>> GetAllMembers();
        Task<MemberData> GetMemberById(int? id);
    }
}
