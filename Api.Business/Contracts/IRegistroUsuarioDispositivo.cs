using Api.Data.Models;
using Api.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Contracts
{
    public interface IRegistroUsuarioDispositivo
    {
        Task<ResponseItemDTO<RegistroUsuarioDispositivo>> Create(RegistroUsuarioDispositivo request);
        Task<ResponseListDTO<RegistroUsuarioDispositivo>> GetList();
        Task<ResponseItemDTO<RegistroUsuarioDispositivo>> GetItem(int? id);
        Task<ResponseItemDTO<RegistroUsuarioDispositivo>> Update(RegistroUsuarioDispositivo request);
        Task<ResponseItemDTO<string>> Delete(int? id);
    }
}
