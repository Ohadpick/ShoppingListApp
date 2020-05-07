using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingListApp.API.Helpers;
using ShoppingListApp.API.Models;

namespace ShoppingListApp.API.Data
{
    public interface IShoppingListRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(GeneralParams generalParams);
        Task<User> GetUser(int id);
        Task<PagedList<Item>> GetItems(GeneralParams generalParams);
        Task<Item> GetItem(int id);
        Task<PagedList<Category>> GetCategories(GeneralParams generalParams);
        Task<Category> GetCategory(int id);
        Task<PagedList<Basket>> GetBaskets(GeneralParams generalParams);
        Task<Basket> GetBasket(int id);
        Task<BasketItem> GetBasketItem(int id);
    }
}