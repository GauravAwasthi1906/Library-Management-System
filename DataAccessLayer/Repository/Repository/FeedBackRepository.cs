using DataAccessLayer.Data;
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

         public async Task<List<Feedback>> GetAllData()
            {
            var data = from i in _appDbContext.feedback
                       join j in _appDbContext.member
                       on i.MemberId equals j.Id
                       select new Feedback
                       {
                           Id = i.Id,
                           MemberId = i.MemberId,
                           Comment = i.Comment,
                           DateSubmitted = i.DateSubmitted,
                           member = new Member
                           {
                               Id = i.Id,
                               Name = j.Name,
                               ContactInfo=j.ContactInfo,
                               MembershipDate=j.MembershipDate,
                           }
                       };
                       
            return await data.ToListAsync();
        }

        public async Task<Feedback> GetData(int? id)
        {
            var data = from i in _appDbContext.feedback 
                       join j in _appDbContext.member
                       on i.MemberId equals j.Id
                       where i.Id == id 
                        select new Feedback
                        {
                            Id = i.Id,
                            MemberId = i.MemberId,
                            Comment = i.Comment,
                            DateSubmitted = i.DateSubmitted,
                            member = new Member
                            {
                                Id = i.Id,
                                Name = j.Name,
                                ContactInfo = j.ContactInfo,
                                MembershipDate = j.MembershipDate,
                            }
                        };
            return await data.FirstAsync();
        }
    }
}
