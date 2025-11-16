using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaReuniao.Api.Infrastructure;

namespace SalaReuniao.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly AppDbContext  _context;

        public HealthController(AppDbContext  context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();

                if (canConnect)
                {
                    return Ok(new
                    {
                        status = "Healthy",
                        database = "Connected",
                        timestamp = DateTime.UtcNow
                    });
                }

                return StatusCode(503, new
                {
                    status = "Unhealthy",
                    database = "Disconnected",
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(503, new
                {
                    status = "Unhealthy",
                    database = "Disconnected",
                    error = ex.Message,
                    timestamp = DateTime.UtcNow
                });
            }
        }
    }
}
