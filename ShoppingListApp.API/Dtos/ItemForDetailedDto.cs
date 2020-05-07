using System;
using System.Collections.Generic;
using ShoppingListApp.API.Models;

namespace ShoppingListApp.API.Dtos
{
    public class ItemForDetailedDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string photoUrl { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int UnitOfMeasureId { get; set; }
    }
}