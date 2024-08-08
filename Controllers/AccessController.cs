using ecoshare_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace ecoshare_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccessController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult TesteAcesso()
        {
            return Ok("Acesso permitido");
        }
    }
}
