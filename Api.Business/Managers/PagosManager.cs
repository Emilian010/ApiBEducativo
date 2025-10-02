using Api.Business.Contracts;
using Api.Business.Response;
using Api.Data.Data;
using Api.Data.Models;
using Api.Models;
using Api.Models.Common;
using Api.Models.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Managers
{
    public class PagosManager : IPagosManager
    {
        private readonly ApplicationDbContext _context;

        public PagosManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseListDTO<PagosDTO>> GetList(int? alumnoid, int? clienteid)
        {
            try
            {
                var param = new SqlParameter[]{
                    new("AlumnoId", alumnoid),
                    new("Cliente", clienteid)};
                var result = _context.Pagos.FromSqlRaw("EXEC GetPagos @AlumnoId,@Cliente ", param).ToListAsync();
             
                return ResponseData.ResponseSuccess(result.Result);
            }
            catch (Exception ex)
            {
                var error = new ErrorDTO { Code = "1001", Message = "Error al consultar los pagos del alumno " + ex.Message.ToString() };
                return ResponseData.ResponseListFailed<PagosDTO>(error);
            }

        }

        
    }
}
