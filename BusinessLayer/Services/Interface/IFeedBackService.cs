using BusinessLayer.DTOs;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interface
{
    public interface IFeedBackService
    {
        Task<ServiceResponse> AddFeedBack(Feedback feedback);
        Task<ServiceResponse> UpdateFeedBack(int? id ,Feedback feedback);
        Task<ServiceResponse> DeleteFeedBack(int? id);
        Task<List<FeedbackData>> GetAllFeedBacks();
        Task<FeedbackData> GetFeedBack(int? id);
    }
}
