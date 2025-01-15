using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository.Interface;

namespace BusinessLayer.Services.Services
{
    public class MemberService : IMemberService
    {
        private readonly ILogger<MemberService> _logger;
        private readonly IGenericRepository<Member> _context;
        public MemberService(ILogger<MemberService> logger, IGenericRepository<Member> context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<ServiceResponse> AddMember(Member member)
        {
            try { 
                var data = await _context.AddNewData(member);
                _logger.LogInformation($" {data.Name} has beed Successfully Added");
                return new ServiceResponse(true, "Member has beed Added Successfully");
               
            } catch (Exception ex) {
                _logger.LogError(ex, $"An error occurred while adding the member: {member}");
                return new ServiceResponse(false,ex.Message);
            }

        }

        public async Task<ServiceResponse> DeleteMember(int? id)
        {
            
            try {
                if (!id.HasValue || id <= 0)
                {
                    _logger.LogWarning("Invalid ID provided.");
                    throw new ArgumentException("ID must be greater than 0.");
                }
                var data = await _context.GetDataById(id);
                if (data != null) { 
                    await _context.DeleteData(data);
                    _logger.LogInformation($"Delete member {id}");
                    return new ServiceResponse(true, "Member Deleted Successfully");
                }
                else
                {
                    _logger.LogWarning($"The Member is not Found with this ID {id}");
                    return new ServiceResponse(false, "Member is not found ");
                }
            } catch (Exception ex) {
                _logger.LogError(ex, $"An error occurred while Deleting the member: {id}");
                return new ServiceResponse(false,ex.Message);
            }
        }

        public async Task<List<Member>> GetAllMembers()
        {
            
            try { 
                var data = await _context.GetAllData();
                if (data == null || !data.Any())
                {
                    throw new Exception("Data Not Found");
                }
                return data;
                
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while fetching data in GetAllMembers.");
                throw new Exception("An error occurred while retrieving members.", ex);
            }
        }

        public async Task<Member> GetMemberById(int? id)
        {
            try {
                if (!id.HasValue || id <= 0)
                {
                    _logger.LogWarning("Invalid ID provided.");
                    throw new ArgumentException("ID must be greater than 0.");
                }
                var data = await _context.GetDataById(id);
                if (data == null)
                {
                    throw new Exception($"Member not found with this ID {id}");
                }
                return data;
            } catch (Exception ex) {
                throw new Exception("An Error Occured while fetching Data");
            }
        }

        public async Task<ServiceResponse> UpdateMember(int? id,Member member)
        {
            if (!id.HasValue || id <= 0)
            {
                _logger.LogWarning("Invalid ID provided.");
                throw new ArgumentException("ID must be greater than 0.");
            }
            try {
                var data = await _context.GetDataById(id);
                if (data != null)
                {
                    data.Name = member.Name;
                    data.ContactInfo = member.ContactInfo;
                    data.MembershipDate = member.MembershipDate;
                    await _context.UpdateDate(data);
                    _logger.LogInformation($" {member.Name} has beed Successfully Updated");
                    return new ServiceResponse(true, "Member has been successfully Updated");
                }
                else
                {
                    _logger.LogWarning($"{member.Name} has not  beed  Updated");
                    return new ServiceResponse(false, "Member can not updated");
                }
            } catch (Exception ex) {
                _logger.LogError(ex, $"An error occurred while updating the member: {member}");
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
