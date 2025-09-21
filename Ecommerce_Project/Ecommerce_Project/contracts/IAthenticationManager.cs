using Ecommerce_Project.Dtos;

namespace Ecommerce_Project.contracts
{
    public interface IAthenticationManager
    {
        public Task<bool>validationUser(UserForAthenticationDtocs user);
        public Task<string> CreateToken();
    }
}
