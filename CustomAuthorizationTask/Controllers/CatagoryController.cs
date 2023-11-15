using CustomAuthorizationTask.Data;
using CustomAuthorizationTask.Dto;
using CustomAuthorizationTask.Models;
using CustomAuthorizationTask.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomAuthorizationTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CatagoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetCata")]
        [Authorize(Policy = Policies.RequireCatagoryView)]
        public async Task<IActionResult> Get()
        {
            var result = await _context.Catagories
                .Include(c => c.Products)  
                .ToListAsync();

            return Ok(result);
        }

        [HttpPost("CreateCata")]
        [Authorize(Policy = Policies.RequireCatagoryCreate)]
        public async Task<IActionResult> PostCategory([FromBody] CatagoryDto catagoryDto)
        {
            var catagory = new Catagory { 
                Id = catagoryDto.CatagoryId,
                CatagoryName = catagoryDto.CategoryName,
                ProductsId = catagoryDto.ProductId
            };

           _context.Catagories.Add(catagory);
           await _context.SaveChangesAsync();

           return CreatedAtAction(nameof(Get), new { id = catagory.Id }, catagory);
        }


        [HttpPut("UpdateCata/{id}")]
        [Authorize(Policy = Policies.RequireCatagoryUpdate)]
        public async Task<IActionResult> Update(int id,CatagoryDto catagoryDto)
        {
            var existingCatagory = await _context.Catagories.FindAsync(id);

            if (existingCatagory == null)
            {
                return NotFound(); 
            }

            existingCatagory.CatagoryName = catagoryDto.CategoryName;
            existingCatagory.ProductsId = catagoryDto.ProductId;

            _context.Entry(existingCatagory).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //delete
        [HttpDelete("CataDelete/{id}")]
        [Authorize(Policy = Policies.RequireCatagoryDelete)]
        public async Task<IActionResult> DeleteCata(int id)
        {
            var catagory = await _context.Catagories.FindAsync(id);
            if (catagory == null)
            {
                return NotFound();
            }

            _context.Catagories.Remove(catagory);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
