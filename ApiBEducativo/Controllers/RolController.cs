using Api.Business.Contracts;
using Api.Data.Data;
using Api.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestionSolicitudesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //  [Authorize]
    public class RolController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IRolManager _RolManager;

        public RolController(ApplicationDbContext context, IRolManager RolManager)
        {
            _context = context;
            _RolManager = RolManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRol([FromBody] IdentityRole Rol)
        {
            var response = new ResponseItemDTO<IdentityRole>();

            if (ModelState.IsValid)
            {
                response = await _RolManager.CreateRol(Rol);
                if (response.data != null)
                {
                    return Ok(response);
                }

            }

            return NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetRolList()
        {
            var response = await _RolManager.GetRolList();
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRolItem(string? id)
        {
            var response = await _RolManager.GetRolItem(id);
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRol(IdentityRole Rol)
        {
            var response = await _RolManager.UpdateRol(Rol);

            if (response.data != null)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRol(string? id)
        {
            var response = await _RolManager.DeleteRol(id);
            if (response.data != null || response.Meta.Messages != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
