using Microsoft.AspNetCore.Mvc;
using EcommerceDDD.Application.DTOs;
using EcommerceDDD.Application.Interfaces;

namespace EcommerceDDD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplicationService _productService;

        public ProductController(IProductApplicationService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Cria um novo produto
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
        {
            try
            {
                var result = await _productService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Busca um produto por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            try
            {
                var result = await _productService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lista todos os produtos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var result = await _productService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Lista produtos com estoque mínimo
        /// </summary>
        [HttpGet("stock/{minStock}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByStock(int minStock)
        {
            var result = await _productService.GetByStockAsync(minStock);
            return Ok(result);
        }

        /// <summary>
        /// Atualiza um produto
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> Update(Guid id, [FromBody] CreateProductDto dto)
        {
            try
            {
                var result = await _productService.UpdateAsync(id, dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Remove um produto
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
} 