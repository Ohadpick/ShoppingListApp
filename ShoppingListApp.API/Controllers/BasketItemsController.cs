using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ShoppingListApp.API.Data;
using ShoppingListApp.API.Dtos;
using ShoppingListApp.API.Helpers;
using ShoppingListApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ShoppingListApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/baskets/{basketId}/[controller]")]
    [ApiController]
    public class BasketItemsController : ControllerBase
    {
        private readonly IShoppingListRepository _repo;
        private readonly IMapper _mapper;
        public BasketItemsController(IShoppingListRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasketItem(int userId, int basketId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var basket = await _repo.GetBasket(basketId);

            if (!basket.BasketItems.Any(i => i.Id == id))
                return Unauthorized();

            var itemFromRepo = await _repo.GetBasketItem(id);

            _repo.Delete(itemFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete the item from the busket");
        }
    }
}
