using AutoMapper;
using Ecommerce_Project.Dtos;
using Ecommerce_Project.Models;

namespace Ecommerce_Project.Managers
{
    public class MappingProfile:Profile
    {

        public MappingProfile() {

            CreateMap<RegistrationDto, User>();
            CreateMap<Review, Review>();
            CreateMap<Order, Order>();
            CreateMap<Product, Product>();

        }

    }
}
