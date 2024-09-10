using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.ProductsFeatures.Commands;
using ProductApi.Application.ProductsFeatures.Commands.CreateProduct;
using ProductApi.Application.ProductsFeatures.Commands.UpdateProduct;
using ProductApi.Application.ProductsFeatures.Commands.DeleteProduct;
using ProductApi.Application.ProductsFeatures.Queries.GetProductById;
using ProductApi.Application.ProductsFeatures.Queries.GetProducts;
using ProductApi.Application.ProductsFeatures.Queries.GetProductsByUserId;
using ProductApi.Application.ProductsFeatures.Queries.GetProductsByCategoryId;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ProductApi.Application.VM;
using ProductApi.Application.Constants;


namespace ProductApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getproducts")]
        public async Task<ActionResult<IEnumerable<ProductVM>>> GetProductsAsync(int page = 1, int pagesize = 10)
        {
            var products = await _mediator.Send(new GetProductsQuery { Page = page, PageSize = pagesize });

            return Ok(products);
        }

        [HttpGet("getproduct/{id}")]
        public async Task<ActionResult<ProductVM>> GetProduct(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery { Id = id });
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("CreateProductWithAutoEmailAndUserIdLoadingFromClaim")]
        [Authorize]
        public async Task<IActionResult> CreateProductWithAutoEmailAndUserIdLoadingFromClaim(CreateProductCommand command)
        {

            command.ManufactureEmail = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            command.UserId = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var productId = await _mediator.Send(command);
            return Ok(productId);

        }

        [HttpPut("updateproduct/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommand command)
        {
            var userId = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            command.Id = id;
            var product = await _mediator.Send(new GetProductByIdQuery { Id = command.Id });

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

        [HttpDelete("deleteproduct/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            var product = await _mediator.Send(new GetProductByIdQuery { Id = id });

            if (product == null)
            {
                return NotFound();
            }

            if (product.UserId != User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value)
            {
                return Forbid();
            }

            await _mediator.Send(new DeleteProductCommand { Id = id });
            return NoContent();
        }
        [HttpGet("getproductsbyuserid/{userId}")]
        public async Task<ActionResult<IEnumerable<ProductVM>>> GetProductsByUserId(string userId, int page = 1, int pageSize = 10)
        {
            var query = new GetProductsByUserIdQuery { UserId = userId, Page = page, PageSize = pageSize };
            var products = await _mediator.Send(query);
            return Ok(products);

        }
        [HttpGet("get-products-by-CategoryId/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductVM>>> GetProductsByCategoryIdAsync(int categoryId, int page = 1, int pageSize = 10)
        {
            var products = await _mediator.
                Send(new GetProductsByCategoryIdQuery { CategoryId = categoryId, Page = page, PageSize = pageSize });
                return Ok(products);
        }
    }
}