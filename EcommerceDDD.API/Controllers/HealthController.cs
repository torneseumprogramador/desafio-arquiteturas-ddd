using Microsoft.AspNetCore.Mvc;

namespace EcommerceDDD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Verifica o status da API
        /// </summary>
        [HttpGet]
        public ActionResult<object> Get()
        {
            return Ok(new
            {
                status = "OK",
                message = "API Ecommerce DDD está funcionando!",
                timestamp = DateTime.UtcNow,
                version = "1.0.0"
            });
        }
    }
} 