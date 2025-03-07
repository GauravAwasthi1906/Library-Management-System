using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository.Interface;
using DataAccessLayer.Repository.Interface;

namespace BusinessLayer.Services.Services
{
    public class MemberService : IMemberService
    {
        private readonly ILogger<MemberService> _logger;
        private readonly IMemberRepository _memberRepository;
        public MemberService(ILogger<MemberService> logger, IMemberRepository memberRepository)
        {
            _logger = logger;
            _memberRepository = memberRepository;
        }
        public async Task<ServiceResponse> AddMember(MemberDTO member)
        {
            try {
                if (member == null || string.IsNullOrWhiteSpace(member.Name) || string.IsNullOrWhiteSpace(member.ContactInfo))
                {
                    return new ServiceResponse(false, "All fields are required");
                }
                var data = await _memberRepository.AddNewMember(member.Name,member.ContactInfo);
                if (data == 1)
                {
                    _logger.LogInformation($" {data} has beed Successfully Added");
                    return new ServiceResponse(true, "Member has beed Added Successfully");
                }
                _logger.LogInformation($" {data} can not be Added");
                return new ServiceResponse(false, "Member can not be Added Successfully");
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
                var data = await _memberRepository.GetMemberById(id ?? 0);
                if (data !=null) {
                    await _memberRepository.DeleteMember(id?? 0);
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

        public async Task<List<MemberData>> GetAllMembers()
        {
            
            try { 
                var data = await _memberRepository.GetAllMembers();
                if (!data.Any())
                {
                    throw new Exception("Data Not Found");
                }
                return data;
                
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while fetching data in GetAllMembers.");
                throw new Exception("An error occurred while retrieving members.", ex);
            }
        }

        public async Task<MemberData> GetMemberById(int? id)
        {
            try {
                if (!id.HasValue || id <= 0)
                {
                    _logger.LogWarning("Invalid ID provided.");
                    throw new ArgumentException("ID must be greater than 0.");
                }
                var data = await _memberRepository.GetMemberById(id?? 0);
                return data;
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse> UpdateMember(int? id, MemberDTO member)
        {
            if (!id.HasValue || id <= 0)
            {
                _logger.LogWarning("Invalid ID provided.");
                throw new ArgumentException("ID must be greater than 0.");
            }
            try {
                var data = await _memberRepository.GetMemberById(id ?? 0);
                if (data != null)
                {
                    var updateMember = await _memberRepository.UpdateMember(data.Id,member.Name,member.ContactInfo);
                    if (updateMember == 1)
                    {    
                        _logger.LogInformation($" {member.Name} has beed Successfully Updated");
                        return new ServiceResponse(true, "Member has been successfully Updated");
                    }
                    _logger.LogInformation($" {member.Name} has can not be Updated");
                    return new ServiceResponse(false, "Member can not be Updated");
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
