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
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IShoppingListRepository _repo;
        private readonly IMapper _mapper;
        public BasketsController(IShoppingListRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetBaskets(int userId, [FromQuery]GeneralParams generalParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var baskets = await _repo.GetBaskets(generalParams);

            var basketToReturns = _mapper.Map<IEnumerable<BasketForListDto>>(baskets);

            Response.AddPagination(baskets.CurrentPage, baskets.PageSize, baskets.TotalCount, baskets.TotalCount);

            return Ok(basketToReturns);
        }

        [HttpGet("{id}", Name = "GetBasket")]
        public async Task<IActionResult> GetBasket(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            
            var basket = await _repo.GetBasket(id);

            var basketToReturn = _mapper.Map<BasketForDetailedDto>(basket);

            return Ok(basketToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int userId, int id, [FromBody]BasketForUpdateDto basketForUpdateDto) 
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            
            var basketFromRepo = await _repo.GetBasket (id);
            
            _mapper.Map(basketForUpdateDto, basketFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updateing basket {id} failed on save");
        }
    }
}
