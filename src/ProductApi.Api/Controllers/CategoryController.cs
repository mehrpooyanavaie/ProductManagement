using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.CategoriesFeatures.Commands.CreateCategory;
using ProductApi.Application.CategoriesFeatures.Queries.GetAllCategories;
using ProductApi.Application.CategoriesFeatures.Queries.GetCategoryById;
using ProductApi.Application.CategoriesFeatures.Commands.UpdateCategory;
using ProductApi.Application.CategoriesFeatures.Commands.DeleteCategory;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ProductApi.Application.VM;
using ProductApi.Domain.Entities;



namespace ProductApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(categories);
        }
        [HttpGet("get-category-byid/{id}")]
        public async Task<ActionResult<CategoryVM>> GetCategoryByIdAsync(int id)
        {
            return await _mediator.Send(new GetCategoryByIdQuery { Id = id });
        }

        [HttpPost("CreateCategory")]
        [Authorize]
        public async Task<IActionResult> CreateCategoryAsync(CreateCategoryCommand command)
        {

            var categoryId = await _mediator.Send(command);
            return Ok(categoryId);

        }

        [HttpPut("update-category/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategoryAsync(int id, UpdateCategoryCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("DeleteCategory-byid/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            await _mediator.Send(new DeleteCategoryCommand { Id = id });
            return NoContent();
        }

    }
}