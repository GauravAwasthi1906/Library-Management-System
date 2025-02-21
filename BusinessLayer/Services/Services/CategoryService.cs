using BusinessLayer.CustomException;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository.Interface;

namespace BusinessLayer.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _generic;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(IGenericRepository<Category> generic, ILogger<CategoryService> logger)
        {
            _generic = generic;
            _logger = logger;
        }
        public async Task<ServiceResponse> AddNewCategory(Category category)
        {
            try {
                var data = await _generic.AddNewData(category);
                if (data==null)
                {
                    _logger.LogWarning("The Data can not be added");
                    return new ServiceResponse(false, "Category data can not be added");
                }
                _logger.LogInformation("Data is Added");
                return new ServiceResponse(true, "Category is added successfully");
            }
            catch (Exception ex)
            {
                return new ServiceResponse(false, $"{ex.Message}");
            }
        }

        public async Task<ServiceResponse> DeleteCategory(int? id)
        {
            
            try {
                if (!id.HasValue || id<=0)
                {
                    return new ServiceResponse(false,"Please enter the valid ");
                }
                var data = await _generic.GetDataById(id);
                if (data== null)
                {
                    return new ServiceResponse(false, $"Category data is not found with Id {id}");
                }
                await _generic.DeleteData(data);
                return new ServiceResponse(true,$"Category data deleted Successfully with Id {id}");

            } catch (Exception ex) {
                return new ServiceResponse(false,$"{ex.Message}");
            }
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try { 
                var data = await _generic.GetAllData();
                _logger.LogInformation($"{data.Count}");
                return data;
            } catch (Exception ex) {
                throw new DataCustomException($"{ex.Message}");
            }
        }

        public async Task<Category> GetCategoryById(int? id)
        {
            
            try {
                if (!id.HasValue || id<=0)
                {
                    throw new DataCustomException($"Please Enter the valid Id {id}");
                }
                var data= await _generic.GetDataById(id);
                return data;
            } catch (Exception ex) {
                throw new DataCustomException(ex.Message);
            }
        }

        public async Task<ServiceResponse> UpdateCategory(int? id, Category category)
        {
            try {
                if (!id.HasValue || id<=0)
                {
                    return new ServiceResponse(false, $"Please Enter the valid Id {id}");
                }
                var entity = await _generic.GetDataById(id);
                if (entity == null)
                {
                    return new ServiceResponse(false,$"The data is not available with Id {id}");
                }
                entity.Name = category.Name;
                entity.Description = category.Description;
                var data = await _generic.UpdateDate(entity);
                if (data== null)
                {
                    return new ServiceResponse(false, $"Data of category can not be updated with Id {id}");
                }
                return new ServiceResponse(true,"Category Data Updated Successfully");
            } catch (Exception ex) {
                return new ServiceResponse(false, $"{ex.Message}");
            }
        }
    }
}
