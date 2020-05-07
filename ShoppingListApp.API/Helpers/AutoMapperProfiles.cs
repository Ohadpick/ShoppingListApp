using System.Linq;
using AutoMapper;
using ShoppingListApp.API.Dtos;
using ShoppingListApp.API.Models;

namespace ShoppingListApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // User
            CreateMap<User, UserForListDto>();
            CreateMap<User, UserForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<UserForRegisterDto, User>();
            
            // Item
            CreateMap<Item, ItemForListDto>();
            CreateMap<Item, ItemForDetailedDto>();

            // Category
            CreateMap<Category, CategoryForListDto>();
            CreateMap<Category, CategoryForDetailedDto>();

            // Basket
            CreateMap<Basket, BasketForListDto>();
            CreateMap<Basket, BasketForDetailedDto>();
            CreateMap<BasketForUpdateDto, Basket>();
        }
    }
}