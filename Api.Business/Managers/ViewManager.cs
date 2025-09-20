using Api.Business.Contracts;
using Api.Business.Response;
using Api.Data.Data;
using Api.Data.Models;
using Api.Models.Common;
using Api.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace Api.Business.Managers
{
    public class ViewManager : IViewManager
    {
        private readonly ApplicationDbContext _context;

        public ViewManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseItemDTO<View>> CreateView(View request)
        {
            var response = new ResponseItemDTO<View>();
            try
            {
                _context.Add(request);
                await _context.SaveChangesAsync();

                return ResponseData.ResponseSuccess(request);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1000", Message = "Error al insertar la vista" };
                return ResponseData.ResponseFailed<View>(error);
            }

        }

        public async Task<ResponseListDTO<View>> GetViewList()
        {
            try
            {
                var items = await _context.Views.Include(x => x.Parent).ToListAsync();

                return ResponseData.ResponseSuccess(items);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1000", Message = "Error al consultar las vistas" };
                return ResponseData.ResponseListFailed<View>(error);
            }

        }

        public async Task<ResponseItemDTO<View>> GetViewItem(int? id)
        {
            try
            {
                var item = await _context.Views.Where(x => x.ViewId == id).FirstAsync();

                return ResponseData.ResponseSuccess(item);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1000", Message = "Error al consultar la vista" };
                return ResponseData.ResponseFailed<View>(error);
            }

        }

        public async Task<ResponseItemDTO<View>> UpdateView(View request)
        {
            var error = new ErrorDTO { Code = "1002", Message = "Error al intentar actualizar la vista" };

            try
            {
                if (request == null)
                {
                    return ResponseData.ResponseFailed<View>(error);
                }

                var item = await _context.Views.Where(x => x.ViewId == request.ViewId).FirstAsync();
                item.Description = request.Description;
                item.Url = request.Url;
                item.Allow = request.Allow;
                item.ParentId = request.ParentId;

                _context.Update(item);

                var resultado = await _context.SaveChangesAsync();

                return resultado > 0 ? ResponseData.ResponseSuccess(request) : ResponseData.ResponseFailed<View>(error);
            }
            catch (Exception ex)
            {

                return ResponseData.ResponseFailed<View>(error);
            }

        }
        public async Task<ResponseItemDTO<string>> DeleteView(int? id)
        {
            var error = new ErrorDTO { Code = "1003", Message = "Error al intentar eliminar la vista" };

            try
            {
                var item = await _context.Views.Where(x => x.ViewId == id).FirstAsync();
                var mensaje = string.Format("{0} Eliminado correctamente", item.Description);

                _context.Views.Remove(item);

                var resultado = await _context.SaveChangesAsync();
                return resultado > 0 ? ResponseData.ResponseSuccess(mensaje) : ResponseData.ResponseFailed<string>(error);
            }
            catch (Exception ex)
            {
                return ResponseData.ResponseFailed<string>(error);
            }
        }
    }
}
