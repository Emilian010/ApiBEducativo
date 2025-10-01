using Api.Business.Contracts;
using Api.Business.Response;
using Api.Data.Data;
using Api.Data.Models;
using Api.Models;
using Api.Models.Common;
using Api.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Managers
{
    public class ClienteManager : IClienteManager
    {
        private readonly ApplicationDbContext _context;

        public ClienteManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseItemDTO<Cliente>> CreateCliente(Cliente request)
        {

            try
            {
                _context.Add(request);
                await _context.SaveChangesAsync();

                return ResponseData.ResponseSuccess(request);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1000", Message = "Error al insertar al cliente" + ex.Message.ToString() };
                return ResponseData.ResponseFailed<Cliente>(error);
            }

        }

        public async Task<ResponseListDTO<Cliente>> GetClienteList()
        {
            try
            {
                var items = await _context.Clientes.ToListAsync();

                return ResponseData.ResponseSuccess(items);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1001", Message = "Error al consultar a los clientes" + ex.Message.ToString() };
                return ResponseData.ResponseListFailed<Cliente>(error);
            }

        }

        public async Task<ResponseItemDTO<Cliente>> GetClienteItem(int? id)
        {
            try
            {
                var item = await _context.Clientes.Where(x => x.Id == id).FirstAsync();

                return ResponseData.ResponseSuccess(item);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1002", Message = "Error al consultar al cliente" + ex.Message.ToString() };
                return ResponseData.ResponseFailed<Cliente>(error);
            }

        }

        public async Task<ResponseItemDTO<Cliente>> UpdateCliente(Cliente request)
        {
            var error = new ErrorDTO { Code = "1003", Message = "Error al intentar actualizar al cliente" };

            try
            {
                if (request == null)
                {
                    return ResponseData.ResponseFailed<Cliente>(error);
                }

                var item = await _context.Clientes.Where(x => x.Id == request.Id).FirstAsync();
                item.Nombre = request.Nombre;
                item.Status = request.Status;
                item.ImplementacionId = request.ImplementacionId;

                _context.Clientes.Update(item);

                var resultado = await _context.SaveChangesAsync();

                return resultado > 0 ? ResponseData.ResponseSuccess(request) : ResponseData.ResponseFailed<Cliente>(error);
            }
            catch (Exception ex)
            {
                var error2 = new ErrorDTO { Code = "1003", Message = "Error al actualizar al cliente " + ex.Message.ToString() };
                return ResponseData.ResponseFailed<Cliente>(error2);
            }

        }

        public async Task<ResponseItemDTO<string>> DeleteCliente(int? id)
        {
            var error = new ErrorDTO { Code = "1004", Message = "Error al intentar eliminar al cliente" };

            try
            {
                var item = await _context.Clientes.Where(x => x.Id == id).FirstAsync();
                var mensaje = string.Format("{0} Eliminado correctamente", item.Nombre);

                _context.Clientes.Remove(item);

                var resultado = await _context.SaveChangesAsync();
                return resultado > 0 ? ResponseData.ResponseSuccess(mensaje) : ResponseData.ResponseFailed<string>(error);
            }
            catch (Exception ex)
            {
                var error2 = new ErrorDTO { Code = "1004", Message = "Error al intentar eliminar al cliente" + ex.Message.ToString() };

                return ResponseData.ResponseFailed<string>(error2);
            }

        }
    }
}
