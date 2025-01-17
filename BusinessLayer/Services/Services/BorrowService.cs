using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository.Interface;
using DataAccessLayer.GenericRepository.Repository;
using DataAccessLayer.Repository.Interface;

namespace BusinessLayer.Services.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly ILogger<BorrowService> _logger;
        private readonly IGenericRepository<Borrow> _genericRepository;
        private readonly IBorrowRepository _repository;
        public BorrowService(IGenericRepository<Borrow> genericRepository, IBorrowRepository repository,ILogger<BorrowService> logger)
        {
            _logger = logger;
            _genericRepository = genericRepository;
            _repository = repository;
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
                await _genericRepository.DeleteData(entity);
                _logger.LogInformation("Data Deleted Successfully");
                return new ServiceResponse(true,"Data Deleted Successfully");

            } catch (Exception ex) {
                _logger.LogError("Something went Wrong");
                return new ServiceResponse(false, ex.Message);
            }
        }

        public async Task<List<BorrowData>> GetAllBorrows()
        {
            try { 
                var data = await _repository.GetAllBorrow();
                if (!data.Any())
                {
                    _logger.LogInformation("Empty Details");
                    throw new Exception("Data not found");
                }
                _logger.LogInformation("Borrow Data");
                return data;
            } catch (Exception ex) {
                _logger.LogError($"{ex.Message}");
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<BorrowData> GetBorrowById(int? id)
        {
            try
            {
                var data = await _repository.GetBorrow(id);
                if (data== null)
                {
                    _logger.LogInformation("Empty Details");
                    throw new Exception("Data not found");
                }
                _logger.LogInformation("Borrow Data");
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                throw new Exception($"{ex.Message}");
            }
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
