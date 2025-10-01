using Api.Business.Contracts;
using Api.Data.Models;
using Api.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace ApiBEducativo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ClienteController : Controller
    {
        private readonly IClienteManager _clienteManager;

        public ClienteController(IClienteManager clienteManager)
        {
            _clienteManager = clienteManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente([FromBody] Cliente Cliente)
        {
            var response = new ResponseItemDTO<Cliente>();

            if (ModelState.IsValid)
            {
                response = await _clienteManager.CreateCliente(Cliente);
                if (response.data != null)
                {
                    return Ok(response);
                }

            }

            return NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetClienteList()
        {
            var response = await _clienteManager.GetClienteList();
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteItem(int? id)
        {
            var response = await _clienteManager.GetClienteItem(id);
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCliente(Cliente Cliente)
        {
            var response = await _clienteManager.UpdateCliente(Cliente);

            if (response.data != null)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int? id)
        {
            var response = await _clienteManager.DeleteCliente(id);
            if (response.data != null || response.Meta.Messages != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
