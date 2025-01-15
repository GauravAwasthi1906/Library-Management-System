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

        public Task<ServiceResponse?> DeleteBorrow(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Borrow>> GetAllBorrows()
        {
            throw new NotImplementedException();
        }

        public Task<Borrow> GetBorrow(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateBorrow(int? id, Borrow borrow)
        {
            throw new NotImplementedException();
        }
    }
}
