using Api.Business.Contracts;
using Api.Business.Response;
using Api.Data.AspNet;
using Api.Data.Data;
using Api.Models.Common;
using Api.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Business.Managers
{
    public class RolManager : IRolManager
    {
        private readonly ApplicationDbContext _context;

        public RolManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseItemDTO<IdentityRole>> CreateRol(IdentityRole request)
        {

            try
            {
                request.Id = Guid.NewGuid().ToString();
                request.ConcurrencyStamp = "1";
                _context.Add(request);
                await _context.SaveChangesAsync();

                return ResponseData.ResponseSuccess(request);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1000", Message = "Error al insertar el servicio" };
                return ResponseData.ResponseFailed<IdentityRole>(error);
            }

        }

        public async Task<ResponseListDTO<IdentityRole>> GetRolList()
        {
            try
            {
                var items = await _context.Roles.ToListAsync();

                return ResponseData.ResponseSuccess(items);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1000", Message = "Error al consultar los servicios" };
                return ResponseData.ResponseListFailed<IdentityRole>(error);
            }

        }

        public async Task<ResponseItemDTO<IdentityRole>> GetRolItem(string? id)
        {
            try
            {
                var item = await _context.Roles.Where(x => x.Id == id).FirstAsync();

                return ResponseData.ResponseSuccess(item);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1000", Message = "Error al consultar los servicios" };
                return ResponseData.ResponseFailed<IdentityRole>(error);
            }

        }

        public async Task<ResponseItemDTO<IdentityRole>> UpdateRol(IdentityRole request)
        {
            var error = new ErrorDTO { Code = "1002", Message = "Error al intentar actualizar el servicio" };

            try
            {
                if (request == null)
                {
                    return ResponseData.ResponseFailed<IdentityRole>(error);
                }

                var item = await _context.Roles.Where(x => x.Id == request.Id).FirstAsync();
                item.Name = request.Name;
                item.NormalizedName = request.NormalizedName;
                item.ConcurrencyStamp = request.ConcurrencyStamp;

                _context.Roles.Update(item);

                var resultado = await _context.SaveChangesAsync();

                return resultado > 0 ? ResponseData.ResponseSuccess(request) : ResponseData.ResponseFailed<IdentityRole>(error);
            }
            catch (Exception ex)
            {

                return ResponseData.ResponseFailed<IdentityRole>(error);
            }

        }

        public async Task<ResponseItemDTO<string>> DeleteRol(string? id)
        {
            var error = new ErrorDTO { Code = "1003", Message = "Error al intentar eliminar los servicios" };

            try
            {
                var item = await _context.Roles.Where(x => x.Id == id).FirstAsync();
                var mensaje = string.Format("{0} Eliminado correctamente", item.Name);

                _context.Roles.Remove(item);

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
