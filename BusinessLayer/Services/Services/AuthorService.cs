using BusinessLayer.CustomException;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository.Interface;
using DataAccessLayer.Repository.Interface;

namespace BusinessLayer.Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ILogger<AuthorService> _logger;
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(ILogger<AuthorService> logger, IAuthorRepository authorRepository)
        {
            _logger = logger;
            _authorRepository = authorRepository;
        }
        public async Task<ServiceResponse> AddAuthorData(Author author)
        {
            try {

                _logger.LogInformation(author.Name,author.Biography);
                var data = await _authorRepository.AddnewData(author);
                if (data == 0)
                {
                    _logger.LogWarning("The Data can not be Added");
                    return new ServiceResponse(false, "Data can not be Added");
                }
                _logger.LogInformation($"The is Added Successfully {data}");
                return new ServiceResponse(true, "Data Added Successfully");    
            } catch (Exception ex) {
                _logger.LogError($"{ex.Message}");
                return new ServiceResponse(false,$"{ex.Message}");
            }
        }

        public async Task<ServiceResponse> DeleteAuthorData(int? id)
        {
            try {
                if (!id.HasValue || id<=0)
                {   
                    _logger.LogWarning($"Please Enter the Valid Id {id}");
                    return new ServiceResponse(false, $"Please provide the Valid Id {id}");
                }
                var data = await _authorRepository.GetById(id);
                if (data== null)
                {
                    _logger.LogDebug($"Data is empty {data}");
                    return new ServiceResponse(false, $"The Data of Author is not available with Id {id}");
                }
                await _authorRepository.DeleteData(id);
                _logger.LogInformation($"Data deleted Successfully {data}");
                return new ServiceResponse (true,$"The Data of Author is Deleted with Id {id}");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, $"{ex.Message}");
            }
        }

        public async Task<List<AuthorData>> GetAllAuthorData()
        {
            try { 
                var data = await _authorRepository.GetAllData();
                if (!data.Any())
                {
                    _logger.LogInformation("Data is not found");
                    throw new DataCustomException("Data not found");
                }
                _logger.LogInformation($"{data.Count} author data");
                return data;
            }
            catch(Exception ex){
                throw new DataCustomException("Something went wrong",ex);
            }
        }

        public async Task<AuthorData> GetAuthorById(int? id)
        {
            try {
                if (!id.HasValue || id<=0)
                {
                    throw new DataCustomException($"Please Pass the Valid Id {id}");
                }
                var data = await _authorRepository.GetById(id);
                return data;
            } catch (Exception ex) {
                throw new DataCustomException("Something went wrong");
            }
        }

        public async Task<ServiceResponse> UpdateAuthorData(int? id, Author author)
        {
            try {
                var data = await _authorRepository.GetById(id);
                if (data == null)
                {
                    _logger.LogInformation($"data not found with this Id {id}");
                    return new ServiceResponse(false,"Data not found");                    
                }
                await _authorRepository.UpdateData(id?? 0,author.Name,author.Biography);
                return new ServiceResponse(true, "Author data updated Successfully");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, $"{ex.Message}");
            }
        }
    }
}
