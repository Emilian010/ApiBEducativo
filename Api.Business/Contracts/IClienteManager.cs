using Api.Data.Models;
using Api.Models;
using Api.Models.Response;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Contracts
{
    public interface IClienteManager
    {
        Task<ResponseItemDTO<Cliente>> CreateCliente(Cliente request);
        Task<ResponseListDTO<Cliente>> GetClienteList();
        Task<ResponseItemDTO<Cliente>> GetClienteItem(int? id);
        Task<ResponseItemDTO<Cliente>> UpdateCliente(Cliente request);
        Task<ResponseItemDTO<string>> DeleteCliente(int? id);
    }
}
