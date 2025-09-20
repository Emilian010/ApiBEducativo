
using Api.Models.Response;
using Microsoft.AspNetCore.Identity;

namespace Api.Business.Contracts
{
    public interface IRolManager
    {
        Task<ResponseItemDTO<IdentityRole>> CreateRol(IdentityRole request);


        Task<ResponseListDTO<IdentityRole>> GetRolList();

        Task<ResponseItemDTO<IdentityRole>> GetRolItem(string? id);

        Task<ResponseItemDTO<IdentityRole>> UpdateRol(IdentityRole request);

        Task<ResponseItemDTO<string>> DeleteRol(string? id);
    }
}
