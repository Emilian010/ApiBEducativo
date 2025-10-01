using Api.Business.Contracts;
using Api.Data.Models;
using Api.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace ApiBEducativo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RegistroUsuarioDispositivoController : Controller
    {
        private readonly IRegistroUsuarioDispositivo _registrouserdispManager;

        public RegistroUsuarioDispositivoController(IRegistroUsuarioDispositivo registrouserdispManager)
        {
            _registrouserdispManager = registrouserdispManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegistroUsuarioDispositivo RegistroUsuarioDispositivo)
        {
            var response = new ResponseItemDTO<RegistroUsuarioDispositivo>();

            if (ModelState.IsValid)
            {
                response = await _registrouserdispManager.Create(RegistroUsuarioDispositivo);
                if (response.data != null)
                {
                    return Ok(response);
                }

            }

            return NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var response = await _registrouserdispManager.GetList();
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteItem(int? id)
        {
            var response = await _registrouserdispManager.GetItem(id);
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCliente(RegistroUsuarioDispositivo RegistroUsuarioDispositivo)
        {
            var response = await _registrouserdispManager.Update(RegistroUsuarioDispositivo);

            if (response.data != null)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int? id)
        {
            var response = await _registrouserdispManager.Delete(id);
            if (response.data != null || response.Meta.Messages != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
