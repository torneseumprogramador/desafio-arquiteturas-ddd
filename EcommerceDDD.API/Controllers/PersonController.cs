using Microsoft.AspNetCore.Mvc;
using EcommerceDDD.Application.DTOs;
using EcommerceDDD.Application.Interfaces;

namespace EcommerceDDD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonApplicationService _personService;

        public PersonController(IPersonApplicationService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// Cria uma nova pessoa física
        /// </summary>
        [HttpPost("individual")]
        public async Task<ActionResult<PersonDto>> CreateIndividual([FromBody] CreateIndividualPersonDto dto)
        {
            try
            {
                var result = await _personService.CreateIndividualAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cria uma nova pessoa jurídica
        /// </summary>
        [HttpPost("corporate")]
        public async Task<ActionResult<PersonDto>> CreateCorporate([FromBody] CreateCorporatePersonDto dto)
        {
            try
            {
                var result = await _personService.CreateCorporateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Busca uma pessoa por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetById(Guid id)
        {
            try
            {
                var result = await _personService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Busca uma pessoa por email
        /// </summary>
        [HttpGet("email/{email}")]
        public async Task<ActionResult<PersonDto>> GetByEmail(string email)
        {
            try
            {
                var result = await _personService.GetByEmailAsync(email);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lista todas as pessoas
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
        {
            var result = await _personService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Lista todas as pessoas físicas
        /// </summary>
        [HttpGet("individuals")]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAllIndividuals()
        {
            var result = await _personService.GetAllIndividualsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Lista todas as pessoas jurídicas
        /// </summary>
        [HttpGet("corporates")]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAllCorporates()
        {
            var result = await _personService.GetAllCorporatesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Atualiza uma pessoa física
        /// </summary>
        [HttpPut("{id}/individual")]
        public async Task<ActionResult<PersonDto>> UpdateIndividual(Guid id, [FromBody] CreateIndividualPersonDto dto)
        {
            try
            {
                var result = await _personService.UpdateAsync(id, dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza uma pessoa jurídica
        /// </summary>
        [HttpPut("{id}/corporate")]
        public async Task<ActionResult<PersonDto>> UpdateCorporate(Guid id, [FromBody] CreateCorporatePersonDto dto)
        {
            try
            {
                var result = await _personService.UpdateAsync(id, dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Remove uma pessoa
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _personService.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
} 