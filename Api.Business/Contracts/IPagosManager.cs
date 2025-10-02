using Api.Models.Response;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Data.Models;

namespace Api.Business.Contracts
{
    public interface IPagosManager
    {
        Task<ResponseListDTO<PagosDTO>> GetList(int? alumnoid,int? clienteid);
    }
}
