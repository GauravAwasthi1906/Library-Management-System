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

        public async Task<int> AddData(int memberId, string comment)
        {
            try {
                return await _appDbContext.Database.ExecuteSqlRawAsync(
                    "EXEC AddNewFeedBack @p0, @p1",
                    memberId, comment);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteData(int? id)
        {
            try {
                return await _appDbContext.Database.ExecuteSqlRawAsync
                    ($"EXEC DeleteFeedBack @p0", id);
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
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

        public async Task<int> UpdateData(int? id,int memberId, string comment)
        {
            try
            {
                return await _appDbContext.Database.ExecuteSqlRawAsync(
                    "EXEC UpdateFeedback @p0, @p1, @p2",
                    id?? 0,memberId, comment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
