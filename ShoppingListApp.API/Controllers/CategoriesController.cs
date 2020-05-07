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
    public class CategoriesController : ControllerBase
    {
        private readonly IShoppingListRepository _repo;
        private readonly IMapper _mapper;
        public CategoriesController(IShoppingListRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(int userId, [FromQuery]GeneralParams generalParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            
            var categories = await _repo.GetCategories(generalParams);

            var categoryToReturns = _mapper.Map<IEnumerable<CategoryForListDto>>(categories);

            Response.AddPagination(categories.CurrentPage, categories.PageSize, categories.TotalCount, categories.TotalCount);

            return Ok(categoryToReturns);
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var category = await _repo.GetCategory(id);

            var categoryToReturn = _mapper.Map<CategoryForDetailedDto>(category);

            return Ok(categoryToReturn);
        }
    }
}
