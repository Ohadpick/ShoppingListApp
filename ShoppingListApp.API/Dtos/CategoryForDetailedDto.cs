using System;
using System.Collections.Generic;
using ShoppingListApp.API.Models;

namespace ShoppingListApp.API.Dtos
{
    public class CategoryForDetailedDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}