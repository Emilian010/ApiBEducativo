using Api.Business.Contracts;
using Api.Business.Response;
using Api.Data.Data;
using Api.Models.Common;
using Api.Models.Response;
using Api.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Api.Business.Managers
{
    public class CalificacionesManager : ICalificacionesManager
    {
        private readonly ApplicationDbContext _context;

        public CalificacionesManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseListDTO<CalificacionesDTO>> GetList(int? alumnoinsid, int? clienteid, int? noeval)
        {
            try
            {
                var param = new SqlParameter[]{
                    new("AlumnoInscritoId", alumnoinsid),
                    new("Cliente", clienteid),
                    new("NoEvaluacion", noeval)
                };
                var result = _context.Calificaciones.FromSqlRaw("EXEC GetCalificaciones @AlumnoInscritoId,@Cliente,@NoEvaluacion ", param).ToListAsync();

                return ResponseData.ResponseSuccess(result.Result);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1001", Message = "Error al consultar las calificaciones del alumno " + ex.Message.ToString() };
                return ResponseData.ResponseListFailed<CalificacionesDTO>(error);
            }

        }

    }
}
