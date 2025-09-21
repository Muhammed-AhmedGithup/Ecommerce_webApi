using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Ecommerce_Project.Controllers
{
    [Route("api/[controller]"),Authorize]
    [ApiController]
    public class ReveiwsController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ReveiwsController(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task< IActionResult> GetAllReviews() {
            var reveiws = await db.Reviews.ToListAsync();

            return Ok(reveiws);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetReview(int id)
        {
            var reveiw=await db.Reviews.FirstOrDefaultAsync(r=>r.Id==id);
            return Ok(reveiw);
        }

        [HttpPost("AddReveiw")]
        public async Task<IActionResult> AddReveiw([FromBody] Review review)
        {
            await db.Reviews.AddAsync(review);
            await db.SaveChangesAsync();
            return StatusCode(201, review);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReveiw(int id,[FromBody]Review review)
        {
            var re =await db.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if(re == null)
            {
                return NotFound();
            }
            re = mapper.Map(review, re);
            await db.SaveChangesAsync();
            return Ok(re);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var rev=await db.Reviews.FirstOrDefaultAsync(r=>r.Id==id);
            if(rev == null)
            {
                return NotFound();
            }
            db.Remove(rev);
            await db.SaveChangesAsync();
            return Ok(rev);
        }



    }
}
