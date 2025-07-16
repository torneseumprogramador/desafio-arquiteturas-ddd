using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceDDD.Infrastructure.Data;

namespace EcommerceDDD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly EcommerceDbContext _context;

        public HealthController(EcommerceDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Verifica o status da API e conexão com banco de dados
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<object>> Get()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var healthStatus = "OK";
            var databaseStatus = "OK";
            var databaseMessage = "Conexão com banco de dados estabelecida com sucesso";
            
            try
            {
                // Testa a conexão com o banco de dados
                var canConnect = await _context.Database.CanConnectAsync();
                if (!canConnect)
                {
                    databaseStatus = "ERROR";
                    databaseMessage = "Não foi possível conectar com o banco de dados";
                    healthStatus = "DEGRADED";
                }
            }
            catch (Exception ex)
            {
                databaseStatus = "ERROR";
                databaseMessage = $"Erro na conexão com banco de dados: {ex.Message}";
                healthStatus = "DEGRADED";
            }

            return Ok(new
            {
                status = healthStatus,
                message = "API Ecommerce DDD está funcionando!",
                timestamp = DateTime.UtcNow,
                version = "1.0.0",
                documentation = $"{baseUrl}/swagger",
                checks = new
                {
                    api = new
                    {
                        status = "OK",
                        message = "API respondendo corretamente"
                    },
                    database = new
                    {
                        status = databaseStatus,
                        message = databaseMessage
                    }
                }
            });
        }
    }
} 