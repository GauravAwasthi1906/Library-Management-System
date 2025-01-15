using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository.Interface;
using DataAccessLayer.GenericRepository.Repository;

namespace BusinessLayer.Services.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly ILogger<Borrow> _logger;
        private readonly IGenericRepository<Borrow> _genericRepository;

        public BorrowService(GenericRepository<Borrow> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<ServiceResponse> AddBorrow(Borrow borrow)
        {
            try { 
                var data =await _genericRepository.AddNewData(borrow);
                if (data==null)
                {
                    _logger.LogWarning("You can not Add this Borrow");
                }
                _logger.LogInformation("Borrow Added Successfully");
                return new ServiceResponse(true, "Borrow Added Successfully");
            } catch (Exception ex) {
                return new ServiceResponse(false, ex.Message);
            }
        }

        public async Task<ServiceResponse?> DeleteBorrow(int? id)
        {
            try {
                if (!id.HasValue || id<=0)
                {
                    _logger.LogWarning("Please Enter the valid Id");
                    return new ServiceResponse(false,"Please Enter The valid Id");
                }
                var entity =await _genericRepository.GetDataById(id);
                if (entity == null) {
                    _logger.LogWarning("Data not found");
                    return new ServiceResponse(false, $"Borrow not found with this ID {id}");
                }
                _genericRepository.DeleteData(entity);
                _logger.LogInformation("Data Deleted Successfully");
                return new ServiceResponse(true,"Data Deleted Successfully");

            } catch (Exception ex) {
                _logger.LogError("Something went Wrong");
                return new ServiceResponse(false, ex.Message);
            }
        }

        public Task<List<Borrow>> GetAllBorrows()
        {
            throw new NotImplementedException();
        }

        public Task<Borrow> GetBorrow(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> UpdateBorrow(int? id, Borrow borrow)
        {
            try {
                if (!id.HasValue || id <= 0)
                {
                    _logger.LogWarning("Please Enter the valid Id");
                    return new ServiceResponse(false, "Please Enter The valid Id");
                }
                var entity = await _genericRepository.GetDataById(id);
                if (entity == null)
                {
                    _logger.LogWarning("Data not found");
                    return new ServiceResponse(false, $"Borrow not found with this ID {id}");
                }
                entity.MemberId = borrow.MemberId;
                entity.BookId = borrow.BookId;
                await _genericRepository.UpdateDate(entity);
                _logger.LogInformation($"{entity.MemberId}");
                return new ServiceResponse(true, "Borrow Updated Successfully");
            } catch (Exception ex) {
                _logger.LogError("Something went wrong");
                return new ServiceResponse(false,ex.Message);
            }
        }
    }
}
