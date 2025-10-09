using Microsoft.AspNetCore.Mvc;

namespace ServicioApiBodegaBalanceado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        public StatusController() { }

        [HttpGet("Status")]
        public IActionResult Status()
        {
            return Ok("Hello World!!!");
        }
    }
}
