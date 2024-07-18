using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.Products.Commands;
using ProductApi.Application.Products.Queries;
using ProductApi.Domain.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ProductApi.Domain.VM;

namespace ProductApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVM>>> GetProducts(int page = 1, int pagesize = 10)
        {
            var products = await _mediator.Send(new GetProductsQuery());
            var totalcount = products.Count();
            var totalpages = (int)Math.Ceiling((decimal)totalcount / pagesize);
            var productsperpage = products.Skip((page - 1) * pagesize).Take(pagesize).ToList();
            return Ok(productsperpage);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVM>> GetProduct(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery { Id = id });
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
        {
            command.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            command.ManufactureEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var productId = await _mediator.Send(command);
            return Ok(productId);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var product = await _mediator.Send(new GetProductByIdQuery { Id = id });

            if (product == null)
            {
                return NotFound();
            }

            if (product.UserId != userId)
            {
                return Forbid();
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var product = await _mediator.Send(new GetProductByIdQuery { Id = id });

            if (product == null)
            {
                return NotFound();
            }

            if (product.UserId != userId)
            {
                return Forbid();
            }

            await _mediator.Send(new DeleteProductCommand { Id = id });
            return NoContent();
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ProductVM>>> GetProductsByUserId(string userId)
        {
            var query = new GetProductsByUserIdQuery { UserId = userId };
            var products = await _mediator.Send(query);
            return Ok(products);
        }
    }
}