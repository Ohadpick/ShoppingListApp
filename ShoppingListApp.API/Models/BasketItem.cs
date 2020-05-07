namespace ShoppingListApp.API.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public virtual Item Item { get; set; }
    }
}