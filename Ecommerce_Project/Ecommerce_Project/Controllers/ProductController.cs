using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController,Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ProductController(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await db.Products.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var prod=await db.Products.FindAsync(id);
            if(prod== null)
            {
                return NotFound();
            }
            return Ok(prod);
        }

        [HttpGet("category/{categoryid:int}")]
        public async Task<IActionResult> GetproductByCategory(int categoryid)
        {
            var catego=await db.Categories.FindAsync(categoryid);
            if(catego== null)
            {
                return NotFound();
            }
            var products =  db.Products.Where(o => o.Category == catego);
            return Ok(products);
        }
        [HttpGet("seller/{sellerId}")]
        public IActionResult GetProductsBySeller(string sellerId)
        {
            User seller = db.Users.Find(sellerId);
            if (seller == null)
            {
                return NotFound();
            }
            var sellersProducts = db.Products.Where(x => x.Seller == seller);
            return Ok(sellersProducts);
        }
        [HttpPost]
        public async Task<IActionResult> NewProduct([FromBody] Product value)
        {
            await db.Products.AddAsync(value);
            await db.SaveChangesAsync();
            return StatusCode(201, value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            var productToDelete = await db.Products.FindAsync(id);
            if (productToDelete == null)
            {
                return NotFound();
            }
            
            db.Products.Remove(productToDelete);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product value)
        {
            
            Product productToChange = await db.Products.FindAsync(value.ProductId);

            if (productToChange == null)
            {
                return NotFound();
            }
            productToChange = mapper.Map(value, productToChange);
            db.Update(productToChange);
            await db.SaveChangesAsync();
            return Ok(productToChange);
        }
    }
}
