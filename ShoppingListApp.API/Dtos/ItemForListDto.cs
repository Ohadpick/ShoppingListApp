using System;

namespace ShoppingListApp.API.Dtos
{
    public class ItemForListDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int UnitOfMeasureId { get; set; }
    }
}