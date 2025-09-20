using Api.Business.Contracts;
using Api.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionSolicitudesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _UserManager;

        public UserController(IUserManager UserManager)
        {

            _UserManager = UserManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var response = await _UserManager.GetUserList();
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserItem(string? id)
        {
            var response = await _UserManager.GetUserItem(id);
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UsuarioData Users)
        {
            var response = await _UserManager.UpdateUser(Users);

            if (response.data != null)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string? id)
        {
            var response = await _UserManager.DeleteUser(id);
            if (response.data != null || response.Meta.Messages != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

    }
}
