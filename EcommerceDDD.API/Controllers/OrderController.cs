using Microsoft.AspNetCore.Mvc;
using EcommerceDDD.Application.DTOs;
using EcommerceDDD.Application.Interfaces;

namespace EcommerceDDD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderApplicationService _orderService;

        public OrderController(IOrderApplicationService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Cria um novo pedido
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto dto)
        {
            try
            {
                var result = await _orderService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Busca um pedido por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(Guid id)
        {
            try
            {
                var result = await _orderService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lista todos os pedidos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var result = await _orderService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Lista pedidos por pessoa
        /// </summary>
        [HttpGet("person/{personId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetByPerson(Guid personId)
        {
            var result = await _orderService.GetByPersonAsync(personId);
            return Ok(result);
        }

        /// <summary>
        /// Lista pedidos por status
        /// </summary>
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetByStatus(string status)
        {
            try
            {
                var result = await _orderService.GetByStatusAsync(status);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Confirma um pedido
        /// </summary>
        [HttpPost("{id}/confirm")]
        public async Task<ActionResult<OrderDto>> Confirm(Guid id)
        {
            try
            {
                var result = await _orderService.ConfirmAsync(id);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Envia um pedido
        /// </summary>
        [HttpPost("{id}/ship")]
        public async Task<ActionResult<OrderDto>> Ship(Guid id)
        {
            try
            {
                var result = await _orderService.ShipAsync(id);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Entrega um pedido
        /// </summary>
        [HttpPost("{id}/deliver")]
        public async Task<ActionResult<OrderDto>> Deliver(Guid id)
        {
            try
            {
                var result = await _orderService.DeliverAsync(id);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cancela um pedido
        /// </summary>
        [HttpPost("{id}/cancel")]
        public async Task<ActionResult<OrderDto>> Cancel(Guid id)
        {
            try
            {
                var result = await _orderService.CancelAsync(id);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Remove um pedido
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _orderService.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
} 