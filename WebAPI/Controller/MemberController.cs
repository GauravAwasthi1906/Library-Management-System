using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _context;
        
        public MemberController(IMemberService context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllMembers()
        {
            try
            {
                var data = await _context.GetAllMembers();
                if (!data.Any())
                {
                   return NotFound("Member data is empty");
                }
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetMemberById([FromRoute]int? id)
        {
            try
            {
                if (!id.HasValue || id<=0)
                {
                    return BadRequest("Please Enter the valid Id");
                }
                var data = await _context.GetMemberById(id?? 0);
                if (data== null)
                {
                    return NotFound($"Data not found with Id->{id}");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddNewMember([FromBody] MemberDTO member)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var data = await _context.AddMember(member);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMember([FromRoute]int id, [FromBody] MemberDTO member)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var data = await _context.UpdateMember(id,member);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMember([FromRoute] int? id)
        {
            try {
                if (!id.HasValue || id<=0)
                {
                    return BadRequest("Invalid Id ");
                }
                var data = await _context.DeleteMember(id);
                if (!data.Flag)
                {
                    return NotFound($"Member not found with Id->{id}");
                }
                return Ok(data);
            } catch (Exception ex) { 
                return StatusCode(500, ex.Message);
            }
        }
            
    }   
}
