using Api.Business.Contracts;
using Api.Data.Data;
using Api.Models;
using Api.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionSolicitudesWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleViewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IRoleViewsManager _RoleViewsManager;

        public RoleViewsController(ApplicationDbContext context, IRoleViewsManager RoleViewsManager)
        {
            _context = context;
            _RoleViewsManager = RoleViewsManager;
        }

        [HttpGet("{idRole}")]
        public async Task<IActionResult> GetRoleViewList(string? idRole)
        {
            var response = await _RoleViewsManager.GetRoleViewList(idRole);
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> SetRoleView([FromBody] List<RoleViewDTO>? OperationRequest)
        {
            var response = new ResponseItemDTO<bool>();

            if (ModelState.IsValid)
            {
                response = await _RoleViewsManager.SetRoleView(OperationRequest);
                if (response.data != null)
                {
                    return Ok(response);
                }

            }

            return NotFound(response);
        }


    }

}
