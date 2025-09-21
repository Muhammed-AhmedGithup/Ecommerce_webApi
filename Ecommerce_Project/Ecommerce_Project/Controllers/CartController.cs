using AutoMapper;
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
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public CartController(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpGet,Authorize(Roles ="Admine")]
        public async Task<IActionResult> GetAll()
        {
            var carts=await db.Carts.ToListAsync();
            return Ok(carts);

        }
        [HttpGet("{id}"),Authorize]
        public async Task<IActionResult> GetByid(int id)
        {
            var cart= await db.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpGet("user/{id}"),Authorize]
        public async Task<IActionResult> GetCartByUserId(string userid) { 
            var user=await db.Users.FindAsync(userid);
            if (user == null)
            {
                return NotFound();
            }
            var usercart = db.Carts.Where(x => x.User == user);

            return Ok(usercart);

        
        }

        [HttpPost,Authorize]
        public async Task<IActionResult> AddCart(Cart cart)
        {
            await db.Carts.AddAsync(cart);
            await db.SaveChangesAsync();
            return Ok(cart);
        }
        [HttpPut,Authorize]
        public async Task<IActionResult> UpdateCart(Cart cart)
        {
            var cartupdate=await db.Carts.FirstOrDefaultAsync(o=>o.Id==cart.Id);
            if (cartupdate == null)
            {
                return NotFound();
            }
            cartupdate = mapper.Map(cart, cartupdate);
            db.Carts.Update(cartupdate);
            await db.SaveChangesAsync();
            return Ok(cart);
        }



    }
}
