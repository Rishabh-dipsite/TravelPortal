using Microsoft.AspNetCore.Mvc;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HealthCheckController : ControllerBase
    {

        private readonly ILogger<SearchController> _logger;
        public HealthCheckController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "HealthCheck")]
        public OkResult Get()
        {
            return Ok();
        }
    }
}