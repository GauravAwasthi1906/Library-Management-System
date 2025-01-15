using DataAccessLayer.Entities;

namespace DataAccessLayer.Repository.Interface
{
    public interface IFeedBackRepository
    {
        Task<List<Feedback>> GetAllData();
        Task<Feedback> GetData(int? id);
    }
}
