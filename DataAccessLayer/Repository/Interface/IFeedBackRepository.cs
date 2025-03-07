using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repository.Interface
{
    public interface IFeedBackRepository
    {
        Task<int> AddData(int memberId,string comment);
        Task<int> UpdateData(int? id,int memberId,string comment);
        Task<int> DeleteData(int? id);
        Task<List<FeedbackData>> GetAllData();
        Task<FeedbackData> GetData(int? id);
    }
}
