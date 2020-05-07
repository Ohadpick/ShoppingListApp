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

namespace ShoppingListApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IShoppingListRepository _repo;
        private readonly IMapper _mapper;
        public ItemsController(IShoppingListRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems(int userId, [FromQuery]GeneralParams generalParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var items = await _repo.GetItems(generalParams);

            var itemToReturns = _mapper.Map<IEnumerable<ItemForListDto>>(items);

            Response.AddPagination(items.CurrentPage, items.PageSize, items.TotalCount, items.TotalCount);

            return Ok(itemToReturns);
        }

        [HttpGet("{id}", Name = "GetItem")]
        public async Task<IActionResult> GetItem(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            
            var item = await _repo.GetItem(id);

            var itemToReturn = _mapper.Map<ItemForDetailedDto>(item);

            return Ok(itemToReturn);
        }
    }
}
