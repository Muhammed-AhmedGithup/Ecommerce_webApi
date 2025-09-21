using Ecommerce_Project.Data;
using Ecommerce_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext context) {
            _db = context;
        }

        [HttpGet,Authorize] 
        public async Task<IActionResult> GetallCategory()
        {
            var categories = await _db.Categories.ToListAsync(); 

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category=await _db.Categories.FirstOrDefaultAsync(o=>o.Id == id); 
            return Ok(category);
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Add([FromBody]Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return Ok(category);

        }

        [HttpDelete("{id}"),Authorize]
        public async Task <IActionResult> DeleteCategory(int id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(o => o.Id == id);
             _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return Ok(category);
        }
    }

}
