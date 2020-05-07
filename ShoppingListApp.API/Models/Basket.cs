using System.Collections.Generic;

namespace ShoppingListApp.API.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual List<BasketItem> BasketItems { get; set; }
        public int userId { get; set; }
        public virtual User User { get; set; }
    }
}