using AutoMapper;
using Ecommerce_Project.contracts;
using Ecommerce_Project.Dtos;
using Ecommerce_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IAthenticationManager athenticationManager;

        public AuthenticationController(UserManager<User> userManager, IMapper mapper, IAthenticationManager athenticationManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.athenticationManager = athenticationManager;
        }



        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationDto registrationDto)
        {
            var user = mapper.Map<User>(registrationDto);
            var resuilt=await userManager.CreateAsync(user,registrationDto.Password);
            if(!resuilt.Succeeded)
            {
                foreach(var i in resuilt.Errors)
                {
                    ModelState.TryAddModelError(i.Code, i.Description);
                }
                return BadRequest(ModelState);
            }
            await userManager.AddToRoleAsync(user, "User");
            return StatusCode(201, user);

        }

        [HttpPost("Login")]
        public async Task <IActionResult> Athenticate(UserForAthenticationDtocs user)
        {
            if(!await athenticationManager.validationUser(user))
            {
                return Unauthorized();
            }

            return Ok(new {token =await athenticationManager.CreateToken()});
        }

    }
}
