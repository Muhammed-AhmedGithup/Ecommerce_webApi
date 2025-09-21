



using Ecommerce_Project.contracts;
using Ecommerce_Project.Dtos;
using Ecommerce_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce_Project.Managers
{
    public class AuthenticationManager : IAthenticationManager
    {
        private readonly UserManager<User> _userManager;
        private User _user;

        public AuthenticationManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> CreateToken()
        {
            List<Claim> claims=await GetClaims();
            var sighningcred=GetSigningCredentials();
            var token = GenerateTokenOption(sighningcred, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        public async Task<bool> validationUser(UserForAthenticationDtocs user)
        {
            _user=await _userManager.FindByNameAsync(user.UserName);
            return (_user!= null && await _userManager.CheckPasswordAsync(_user,user.Password));
        }

        private async Task< List<Claim>> GetClaims()
        {
            List < Claim > claims= new List<Claim>
            {
                new Claim("email",_user.Email),
                new Claim("id",_user.Id),
                new Claim("userName",_user.UserName)
            };

            var roles =await _userManager.GetRolesAsync(_user);

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }



        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes("SocialMediaWebAPISecretkjvdsbkfsdukhfkscjkfduidhfvfh,vdt");
            var secret= new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private JwtSecurityToken GenerateTokenOption(SigningCredentials signingCredentials,List<Claim> claims)
        {
            var tokenoption = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                audience: "https://localhost:7202",
                issuer: "https://localhost:7202",
                expires: DateTime.Now.AddHours(1),
                claims:claims
                ) ;


            return tokenoption ;
            

        }
    }
}
