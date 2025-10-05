using Api.Business.Contracts;
using Api.Business.Response;
using Api.Data.Data;
using Api.Models;
using Api.Models.Common;
using Api.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace Api.Business.Managers
{
    public class NotificacionesManager : INotificacionesManager
    {
        private readonly ApplicationDbContext _context;

        public NotificacionesManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseListDTO<NotificacionDTO>> GetList()
        {
            try
            {
                var result = _context.Notificaciones.FromSqlRaw("select 1 as Id,'Notificacion 1' as Titulo, 'Confirmación Acceso' as Mensaje, 'el alumno fue confirmado correctamente' as Detalle").ToListAsync(); //_context.Notificaciones.FromSqlRaw("EXEC GetNotificaciones").ToListAsync();

                return ResponseData.ResponseSuccess(result.Result);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1001", Message = "Error al consultar las calificaciones del alumno " + ex.Message.ToString() };
                return ResponseData.ResponseListFailed<NotificacionDTO>(error);
            }

        }
    }
}
