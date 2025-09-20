using Api.Business.Contracts;
using Api.Data.Models;
using Api.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionSolicitudesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ViewController : Controller
    {
        private readonly IViewManager _viewManager;

        public ViewController(IViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateView([FromBody] View View)
        {
            var response = new ResponseItemDTO<View>();

            if (ModelState.IsValid)
            {
                response = await _viewManager.CreateView(View);
                if (response.data != null)
                {
                    return Ok(response);
                }

            }

            return NotFound(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewList()
        {
            var response = await _viewManager.GetViewList();
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetViewItem(int? id)
        {
            var response = await _viewManager.GetViewItem(id);
            if (response.data != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateView(View View)
        {
            var response = await _viewManager.UpdateView(View);

            if (response.data != null)
            {
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteView(int? id)
        {
            var response = await _viewManager.DeleteView(id);
            if (response.data != null || response.Meta.Messages != null)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
