using Microsoft.AspNetCore.Mvc;

namespace ServicioApiBodegaBalanceado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController() {

        }

        [HttpGet]
        public ActionResult Index()
        {
            return new JsonResult("Contenido Devuelto");
        }
    }
}
