using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repository.Interface
{
    public interface IFeedBackRepository
    {
        Task<List<FeedbackData>> GetAllData();
        Task<FeedbackData> GetData(int? id);
    }
}
