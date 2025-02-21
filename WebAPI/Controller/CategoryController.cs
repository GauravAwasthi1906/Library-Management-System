using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllCategoryDetail()
        {
            try
            {
                var data = await _categoryService.GetAllCategories();
                if (!data.Any())
                {
                    return NotFound(new{message= "Category Data is not found"});
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryDetailById([FromRoute]int? id)
        {
            try {
                if (!id.HasValue || id<=0)
                {
                    return BadRequest(new {message = $"Please Enter the valid Id {id}" });
                }
                var data = await _categoryService.GetCategoryById(id);
                if (data== null)
                {
                    return NotFound(new {message= $"The Category Data is not available with Id {id}" });
                }
                return Ok(data);
            }
            catch(Exception ex) {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddnewCategory([FromBody]CategoryDTO category)
        {
            try {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = new Category
                {
                    Name = category.Name,
                    Description = category.Description,
                };
                var data = await _categoryService.AddNewCategory(entity);
                if (!data.Flag)
                {
                    return BadRequest(new {message="This category Data can not be added"});
                }
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategroyDetail([FromRoute]int? id, [FromBody] CategoryDTO category)
        {
            try {
                if (!id.HasValue|| id<=0)
                {
                    return BadRequest($"Please Pass the valid Id {id}");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = new Category
                {
                    Name=category.Name,
                    Description=category.Description,
                };
                var data = await _categoryService.UpdateCategory(id, entity);
                if (!data.Flag)
                {
                    return BadRequest(new {message= $"{data.Message}" });
                }
                return Ok(data);
            } catch(Exception ex) {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryDetail([FromRoute] int? id)
        {
            try {
                if (!id.HasValue || id<=0)
                {
                    return BadRequest(new {message=  $"Please enter the valid Id {id}"});
                }
                var data = await _categoryService.DeleteCategory(id);
                if (!data.Flag)
                {
                    return NotFound(new {message= $"{data.Message}" });
                }
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
