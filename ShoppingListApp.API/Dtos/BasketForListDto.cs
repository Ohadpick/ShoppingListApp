using System;
using System.Collections.Generic;
using ShoppingListApp.API.Models;

namespace ShoppingListApp.API.Dtos
{
    public class BasketForListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}