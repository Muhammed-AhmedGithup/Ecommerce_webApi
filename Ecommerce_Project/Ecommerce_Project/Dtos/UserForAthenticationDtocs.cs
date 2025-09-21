using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Project.Dtos
{
    public class UserForAthenticationDtocs
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Password { get; set; }
    }
}
