using Microsoft.AspNetCore.Identity;

namespace Ecommerce_Project.Models
{
    public class User:IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
