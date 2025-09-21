using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.Dtos;
using Ecommerce_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ecommerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public OrderController(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpGet,Authorize(Roles ="Admine")]
        public async Task< IActionResult> GetAll()
        {
            return Ok(await db.Orders.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order=await db.Orders.FirstOrDefaultAsync(o=>o.OrderId == id);
            if(order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpGet("customer/{customerid}"),Authorize]
        public async Task<IActionResult> GetordersbyCustomer(string customerid)
        {
            var customer = await db.Users.FindAsync(customerid);
            if(customer == null)
            {
                return NotFound();
            }
            var customerOrders= db.Orders.Where(o=>o.User==customer);
           
            return Ok(customerOrders);

        }

        [HttpGet("Date"),Authorize(Roles ="Admine")]
        public  IActionResult GetByDate(DateDots date)
        {
            DateTime dateTime = new DateTime(year:date.Year, month: date.Month, day: date.Day
                , hour: date.Hours, minute: date.Minutes,
                 second:date.Seconds );

            var orders= db.Orders.Where(o=>o.Date==dateTime);
            return Ok(orders);
        }

        [HttpPut,Authorize(Roles ="Admine")]
        public async Task<IActionResult> Update([FromBody]Order order)
        {
            var orderupdate=await db.Orders.FirstOrDefaultAsync(o=>o.OrderId==order.OrderId);
            if(orderupdate == null)
            {
                return NotFound();
            }
            orderupdate = mapper.Map(order, orderupdate);
            db.Update(orderupdate);
            await db.SaveChangesAsync();
            return Ok(orderupdate);
        }

        [HttpPost,Authorize(Roles ="Admine")]
        public async Task<IActionResult> Addorder(OrderDots order)
        {
            DateTime dateTime = new DateTime(year: order.Year, month: order.Month, day: order.Day
            , hour: order.Hours, minute: order.Minutes,
            second: order.Seconds);

            Order neworder = new Order()
            {
                UserId=order.UserId,
                Date=dateTime

            };

            await db.Orders.AddAsync(neworder);
            await db.SaveChangesAsync();
            return Ok(neworder);
        }

        [HttpDelete("id"),Authorize(Roles ="Admine")]
        public async Task<IActionResult>Delete(int id)
        {
            var order = await db.Orders.FirstOrDefaultAsync(o=>o.OrderId == id);
            if( order == null )
            {
                return NotFound();
            }
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            return Ok(order);

        }

    }
}
