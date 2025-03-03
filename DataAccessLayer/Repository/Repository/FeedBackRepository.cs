using DataAccessLayer.Data;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Repository
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private readonly AppDbContext _appDbContext;
        
        public FeedBackRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

         public async Task<List<FeedbackData>> GetAllData()
            {
                var feedback = await _appDbContext.Database
                .SqlQueryRaw<FeedbackData>("EXEC GetAllFeedback")
                .ToListAsync();

                return feedback;
            }

        public async Task<FeedbackData?> GetData(int? id)
        {
            var feedbackList = await _appDbContext.Database
                .SqlQueryRaw<FeedbackData>("EXEC GetFeedbackById @p0", id)
                .ToListAsync();

            return feedbackList.FirstOrDefault();
        }

    }
}
