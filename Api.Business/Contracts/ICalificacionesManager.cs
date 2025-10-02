using Api.Models.Response;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Contracts
{
    public interface ICalificacionesManager
    {
        Task<ResponseListDTO<CalificacionesDTO>> GetList(int? alumnoinsid, int? clienteid, int? noeval);
    }
}
