using Api.Business.Contracts;
using Api.Models.Response;
using Api.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GestionSolicitudesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager, UserManager<IdentityUser> userManager)
        {
            _authManager = authManager;
            _userManager = userManager;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<ResponseItemDTO<UsuarioData>>> Register([FromBody] UserLogin userLogin)
        {
            var response = await _authManager.UserRegister(userLogin);
            if (response.data != null || response.Meta.Messages != null)
            {
                return Ok(response);
            }
            return NotFound(response);

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials credentials)
        {

            var response = await _authManager.Login(credentials);
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetUser(string? userName)
        {
            var response = await _authManager.GetUser(userName);
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut]
        [Route("UpdatePhoneUser")]
        public async Task<IActionResult> UpdatePhoneUser(UsuarioData user)
        {
            var response = await _authManager.UpdatePhoneUser(user);

            if (response.data != null)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpPut]
        [Route("UpdatePwdUser")]
        public async Task<IActionResult> UpdatePwdUser(UserLogin user)
        {
            var response = await _authManager.UpdatePasswordUser(user);

            if (response.data != null)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            // Well, What do you want to do here ?
            // Wait for token to get expired OR 
            // Maintain token cache and invalidate the tokens after logout method is called
            return Ok(new { Token = "", Message = "Logged Out" });
        }








    }
}
