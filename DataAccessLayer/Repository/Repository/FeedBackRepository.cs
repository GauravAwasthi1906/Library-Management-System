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
            var data = from i in _appDbContext.feedback
                       join j in _appDbContext.member
                       on i.MemberId equals j.Id
                       select new FeedbackData
                       {
                           Id = i.Id,
                           MemberId = i.MemberId,
                           MemberName = j.Name,
                           MemberContactInfo = j.ContactInfo,
                           MembershipDate = j.MembershipDate,
                           DateSubmitted = i.DateSubmitted,
                           Comment = i.Comment,
                       };

            return await data.ToListAsync();
        }

        public async Task<FeedbackData> GetData(int? id)
        {
            var data = from i in _appDbContext.feedback 
                       join j in _appDbContext.member
                       on i.MemberId equals j.Id
                       where i.Id == id 
                        select new FeedbackData
                        {
                            Id = i.Id,
                            MemberId= i.MemberId,
                            MemberName= j.Name,
                            MemberContactInfo=j.ContactInfo,
                            MembershipDate=j.MembershipDate,
                            DateSubmitted=i.DateSubmitted,
                            Comment=i.Comment,
                        };
            return await data.FirstAsync();
        }
    }
}
