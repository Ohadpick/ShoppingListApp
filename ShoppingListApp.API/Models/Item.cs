using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShoppingListApp.API.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int? UnitOfMeasureId { get; set; }
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }
    }
}