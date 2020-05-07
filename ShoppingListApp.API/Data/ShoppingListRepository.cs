using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingListApp.API.Helpers;
using ShoppingListApp.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ShoppingListApp.API.Data
{
    public class ShoppingListRepository : IShoppingListRepository
    {
        private readonly DataContext _context;
        public ShoppingListRepository(DataContext context)
        {
            _context = context;

        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<PagedList<User>> GetUsers(GeneralParams generalParams)
        {
            var users = _context.Users.OrderByDescending(u => u.LastActive).AsQueryable();

            if (!string.IsNullOrEmpty(generalParams.OrderBy))
            {
                switch (generalParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(users, generalParams.PageNumber, generalParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Item> GetItem(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);

            return item;
        }

        public async Task<PagedList<Item>> GetItems(GeneralParams generalParams)
        {
            var items = _context.Items.OrderByDescending(i => i.Description).AsQueryable();

            if (!string.IsNullOrEmpty(generalParams.OrderBy))
            {
                switch (generalParams.OrderBy)
                {
                    case "category":
                        items = items.OrderByDescending(i => i.Category.Description);
                        break;
                    case "price":
                        items = items.OrderByDescending(i => i.Price);
                        break;
                    default:
                        items = items.OrderByDescending(i => i.Description);
                        break;
                }
            }

            if (generalParams.Category != null)
            {
                items = items.Where(i => i.CategoryId == generalParams.Category);
            }

            return await PagedList<Item>.CreateAsync(items, generalParams.PageNumber, generalParams.PageSize);
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            return category;
        }

        public async Task<PagedList<Category>> GetCategories(GeneralParams generalParams)
        {
            var categories = _context.Categories.OrderByDescending(c => c.Description).AsQueryable();

            return await PagedList<Category>.CreateAsync(categories, generalParams.PageNumber, generalParams.PageSize);
        }

        public async Task<Basket> GetBasket(int id)
        {
            var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.Id == id);

            return basket;
        }

        public async Task<PagedList<Basket>> GetBaskets(GeneralParams generalParams)
        {
            var baskets = _context.Baskets.OrderBy(b => b.Title).AsQueryable();

            if (!string.IsNullOrEmpty(generalParams.OrderBy))
            {
                switch (generalParams.OrderBy)
                {
                    case "id":
                        baskets = baskets.OrderByDescending(b => b.Id);
                        break;
                    default:
                        baskets = baskets.OrderBy(b => b.Title);
                        break;
                }
            }

            return await PagedList<Basket>.CreateAsync(baskets, generalParams.PageNumber, generalParams.PageSize);
        }

        public async Task<BasketItem> GetBasketItem(int Id)
        {
            var basketItem = await _context.BasketItems.FirstOrDefaultAsync(i => i.Id == Id);

            return  basketItem;
        }
    }
}