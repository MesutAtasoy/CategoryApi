using Microsoft.AspNetCore.Mvc;

namespace CategoryApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpHead(Name = "Ping")]
        public ActionResult Get()
        {
            return Ok("Pong");
        }
    }
}