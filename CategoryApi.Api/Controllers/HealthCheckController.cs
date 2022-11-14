using Microsoft.AspNetCore.Mvc;

namespace CategoryApi.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        /// <summary>
        /// This ep is responsible for health checking
        /// </summary>
        /// <returns></returns>
        [HttpHead(Name = "Ping")]
        public ActionResult Get()
        {
            return Ok("Pong");
        }
    }
}