using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Project.Dtos
{
    public class OrderDots:DateDots
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserId { get; set; }
    }
}
