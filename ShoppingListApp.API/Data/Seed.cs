using System.Collections.Generic;
using System.Linq;
using ShoppingListApp.API.Models;
using Newtonsoft.Json;
using System;

namespace ShoppingListApp.API.Data
{
    public class Seed
    {
        public static void SeedUsers (DataContext context)
        {
            if (!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordhash, passwordsalt;
                    CreatePasswordHash("password", out passwordhash, out passwordsalt);

                    user.PasswordHash = passwordhash;
                    user.PasswordSalt = passwordsalt;
                    user.UserName = user.UserName.ToLower();
                    context.Users.Add (user);
                }

                context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static void SeedUnitOfMeasures (DataContext context)
        {
            if (!context.UnitOfMeasures.Any())
            {
                var unitOfMeasureData = System.IO.File.ReadAllText("Data/UnitOfMeasureSeedData.json");
                var unitOfMeasures = JsonConvert.DeserializeObject<List<UnitOfMeasure>>(unitOfMeasureData);
                foreach (var unitOfMeasure in unitOfMeasures)
                {
                    context.UnitOfMeasures.Add (unitOfMeasure);
                }

                context.SaveChanges();
            }
        }

        public static void SeedItems (DataContext context)
        {
            if (!context.Items.Any())
            {
                var itemData = System.IO.File.ReadAllText("Data/ItemSeedData.json");
                var items = JsonConvert.DeserializeObject<List<Item>>(itemData);
                foreach (var item in items)
                {
                    context.Items.Add (item);
                }

                context.SaveChanges();
            }
        }

        public static void SeedCategories (DataContext context)
        {
            if (!context.Categories.Any())
            {
                var categoryData = System.IO.File.ReadAllText("Data/CategorySeedData.json");
                var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData);
                foreach (var category in categories)
                {
                    context.Categories.Add (category);
                }

                context.SaveChanges();
            }
        }

        public static void SeedBaskets (DataContext context)
        {
            if (!context.Baskets.Any())
            {
                var basketData = System.IO.File.ReadAllText("Data/BasketSeedData.json");
                var baskets = JsonConvert.DeserializeObject<List<Basket>>(basketData);
                foreach (var basket in baskets)
                {
                    context.Baskets.Add (basket);
                }

                context.SaveChanges();
            }
        }
    }
}