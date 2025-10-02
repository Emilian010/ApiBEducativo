using Api.Business.Contracts;
using Api.Business.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ApiBEducativo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PagosController : Controller
    {
        private readonly IPagosManager _pagosManager;

        public PagosController(IPagosManager pagosManager)
        {
            _pagosManager = pagosManager;
        }
        [HttpGet("{alumnoid}/{clienteid}")]
        public async Task<IActionResult> GetList(int? alumnoid, int? clienteid)
        {
            var response = await _pagosManager.GetList(alumnoid, clienteid);
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

    }
}
