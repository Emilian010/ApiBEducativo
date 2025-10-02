using Api.Business.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ApiBEducativo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CalificacionesController : Controller
    {
        private readonly ICalificacionesManager _calificacionesManager;

        public CalificacionesController(ICalificacionesManager calificacionesManager)
        {
            _calificacionesManager = calificacionesManager;
        }
        [HttpGet("{alumnoinsid}/{clienteid}/{noeval}")]
        public async Task<IActionResult> GetList(int? alumnoinsid, int? clienteid, int? noeval)
        {
            var response = await _calificacionesManager.GetList(alumnoinsid, clienteid, noeval);
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

    }
}
