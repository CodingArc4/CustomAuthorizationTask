using CustomAuthorizationTask.Data;
using CustomAuthorizationTask.Dto;
using CustomAuthorizationTask.Models;
using CustomAuthorizationTask.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomAuthorizationTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCatagoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SubCatagoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        //get
        [HttpGet("GetSubCatagories")]
        [Authorize(Policy = Policies.RequireSubCatagoryView)]
        public async Task<IActionResult> GetSub()
        {
            var result = await _context.SubCatagories.ToListAsync();
            return Ok(result);
        }

        //add
        [HttpPost("CreateSubCatagories")]
        [Authorize(Policy = Policies.RequireSubCatagoryCreate)]
        public async Task<IActionResult> CreateSub(SubCataDto subCataDto)
        {
            var subcata = new SubCatagory { 
                Id = subCataDto.Id,
                SubName = subCataDto.SubName,
                CatagoryId = subCataDto.CatagoryId
            };

            _context.SubCatagories.Add(subcata);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("UpdateSub/{id}")]
        [Authorize(Policy = Policies.RequireSubCatagoryUpdate)]
        public async Task<IActionResult> Update(int id, SubCataDto subCataDto)
        {
            var existingSubCatagory = await _context.SubCatagories.FindAsync(id);

            if (existingSubCatagory == null)
            {
                return NotFound();
            }

            existingSubCatagory.SubName = subCataDto.SubName;
            existingSubCatagory.CatagoryId = subCataDto.CatagoryId;

            _context.Entry(existingSubCatagory).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //delete
        [HttpDelete("SubCataDelete/{id}")]
        [Authorize(Policy = Policies.RequireSubCatagoryDelete)]
        public async Task<IActionResult> DeleteSubCata(int id)
        {
            var Subcatagory = await _context.SubCatagories.FindAsync(id);
            if (Subcatagory == null)
            {
                return NotFound();
            }

            _context.SubCatagories.Remove(Subcatagory);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
