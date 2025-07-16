using Microsoft.AspNetCore.Mvc;

namespace EcommerceDDD.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Página inicial da API
        /// </summary>
        [HttpGet]
        public ActionResult<object> Get()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            
            return Ok(new
            {
                title = "🛒 Ecommerce DDD API",
                description = "API desenvolvida com Domain-Driven Design (DDD) para o curso de Arquiteturas de Software",
                version = "1.0.0",
                author = "Prof. Danilo Aparecido",
                course = "Arquiteturas de Software Modernas",
                platform = "Torne-se um Programador",
                links = new
                {
                    health = $"{baseUrl}/api/health",
                    swagger = $"{baseUrl}/swagger",
                    documentation = $"{baseUrl}/swagger/index.html"
                },
                endpoints = new
                {
                    persons = $"{baseUrl}/api/pessoas",
                    products = $"{baseUrl}/api/produtos",
                    orders = $"{baseUrl}/api/pedidos"
                },
                architecture = new
                {
                    pattern = "Domain-Driven Design (DDD)",
                    layers = new[] { "API", "Application", "Domain", "Infrastructure" },
                    database = "SQL Server",
                    orm = "Entity Framework Core"
                },
                timestamp = DateTime.UtcNow
            });
        }
    }
} 