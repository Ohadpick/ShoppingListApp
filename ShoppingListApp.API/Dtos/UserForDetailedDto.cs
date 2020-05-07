using System;
using System.Collections.Generic;
using ShoppingListApp.API.Models;

namespace ShoppingListApp.API.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string photoUrl { get; set; }
    }
}