using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackService _context;
        
        public FeedBackController(IFeedBackService context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllFeedback()
        {
            try
            {
                var data = await _context.GetAllFeedBacks();
                if (!data.Any())
                {
                    return NotFound("Data not found ");
                }
                return Ok(data);
            }
            catch (Exception ex) {
                return StatusCode(500,ex.Message);
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFeedBackById([FromRoute]int? id)
        {
            try {
                if (!id.HasValue || id<=0)
                {
                    return BadRequest("Please Enter the Valid Id");
                }
                var data = await _context.GetFeedBack(id);
                if (data==null)
                {
                    return NotFound(new {message=$"The data is not found with Id {id}"});
                }
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }   

        [HttpPost]
        public async Task<ActionResult> AddFeedBack([FromBody] FeedbackDTO feedback)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = new Feedback
                {
                    MemberId = feedback.MemberId,
                    Comment = feedback.Comment,
                    DateSubmitted = feedback.DateSubmitted,
                };
                var data = await _context.AddFeedBack(entity);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFeedBack([FromRoute] int? id)
        {
            try {
                if (!id.HasValue || id <= 0)
                {
                    return BadRequest("Please Enter the Valid Id");
                }
                var data =await _context.DeleteFeedBack(id);
                if (!data.Flag)
                {
                    return NotFound(new {message=$"{data.Message}"});                    
                }
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFeedBack([FromRoute] int? id, [FromBody] FeedbackDTO feedback)
        {
            try {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!id.HasValue || id <= 0)
                {
                    return BadRequest("Please Enter the Valid Id");
                }
                var entity = new Feedback
                {
                    MemberId = feedback.MemberId,
                    Comment = feedback.Comment,
                    DateSubmitted = feedback.DateSubmitted,
                };
                var data = await _context.UpdateFeedBack(id, entity);
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);            
            }
        }
    }
}
