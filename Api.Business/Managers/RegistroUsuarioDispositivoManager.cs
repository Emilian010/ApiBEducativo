using Api.Business.Contracts;
using Api.Business.Response;
using Api.Data.Data;
using Api.Data.Models;
using Api.Models.Common;
using Api.Models.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Managers
{
    public class RegistroUsuarioDispositivoManager : IRegistroUsuarioDispositivoManager
    {
        private readonly ApplicationDbContext _context;

        public RegistroUsuarioDispositivoManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseItemDTO<RegistroUsuarioDispositivo>> Create(RegistroUsuarioDispositivo request)
        {

            try
            {
                _context.Add(request);
                await _context.SaveChangesAsync();

                return ResponseData.ResponseSuccess(request);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1000", Message = "Error al insertar registro dispositivo" + ex.Message.ToString() };
                return ResponseData.ResponseFailed<RegistroUsuarioDispositivo>(error);
            }

        }

        public async Task<ResponseListDTO<RegistroUsuarioDispositivo>> GetList()
        {
            try
            {
                var items = await _context.RegistroUsuarioDispositivo.ToListAsync();

                return ResponseData.ResponseSuccess(items);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1000", Message = "Error al consultar los registros dispositivos" + ex.Message.ToString() };
                return ResponseData.ResponseListFailed<RegistroUsuarioDispositivo>(error);
            }

        }

        public async Task<ResponseItemDTO<RegistroUsuarioDispositivo>> GetItem(int? id)
        {
            try
            {
                var item = await _context.RegistroUsuarioDispositivo.Where(x => x.Id == id).FirstAsync();

                return ResponseData.ResponseSuccess(item);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1002", Message = "Error al consultar el registro dispositivo" + ex.Message.ToString() };
                return ResponseData.ResponseFailed<RegistroUsuarioDispositivo>(error);
            }

        }

        public async Task<ResponseItemDTO<RegistroUsuarioDispositivo>> Update(RegistroUsuarioDispositivo request)
        {
            var error = new ErrorDTO { Code = "1003", Message = "Error al intentar actualizar el registro dispositivo" };

            try
            {
                if (request == null)
                {
                    return ResponseData.ResponseFailed<RegistroUsuarioDispositivo>(error);
                }

                var item = await _context.RegistroUsuarioDispositivo.Where(x => x.Id == request.Id).FirstAsync();
                item.DispositivoId = request.DispositivoId;
                item.ClienteId = request.ClienteId;

                _context.RegistroUsuarioDispositivo.Update(item);

                var resultado = await _context.SaveChangesAsync();

                return resultado > 0 ? ResponseData.ResponseSuccess(request) : ResponseData.ResponseFailed<RegistroUsuarioDispositivo>(error);
            }
            catch (Exception ex)
            {
                var error2 = new ErrorDTO { Code = "1003", Message = "Error al actualizar el registro dispositivo " + ex.Message.ToString() };
                return ResponseData.ResponseFailed<RegistroUsuarioDispositivo>(error2);
            }

        }

        public async Task<ResponseItemDTO<string>> Delete(int? id)
        {
            var error = new ErrorDTO { Code = "1004", Message = "Error al intentar eliminar el registro dispositivo" };

            try
            {
                var item = await _context.RegistroUsuarioDispositivo.Where(x => x.Id == id).FirstAsync();
                var mensaje = string.Format("{0} Eliminado correctamente", item.DispositivoId);

                _context.RegistroUsuarioDispositivo.Remove(item);

                var resultado = await _context.SaveChangesAsync();
                return resultado > 0 ? ResponseData.ResponseSuccess(mensaje) : ResponseData.ResponseFailed<string>(error);
            }
            catch (Exception ex)
            {
                var error2 = new ErrorDTO { Code = "1004", Message = "Error al intentar eliminar el registro dispositivo" + ex.Message.ToString() };

                return ResponseData.ResponseFailed<string>(error2);
            }

        }
    }
}
