using Api.Business.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ApiBEducativo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : Controller
    {
        private readonly INotificacionesManager _notificacionesManager;

        public NotificacionesController(INotificacionesManager notificacionesManager)
        {
            _notificacionesManager = notificacionesManager;
        }
        [HttpGet()]
        public async Task<IActionResult> GetList()
        {
            var response = await _notificacionesManager.GetList();
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
