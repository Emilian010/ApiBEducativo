
using Api.Models.Response;
using Api.Models.User;

namespace Api.Business.Contracts
{
    public interface IUserManager
    {
        Task<ResponseItemDTO<UsuarioData>> UpdateUser(UsuarioData request);
        Task<ResponseListDTO<UsuarioData>> GetUserList();
        Task<ResponseItemDTO<UsuarioData>> GetUserItem(string? id);
        Task<ResponseItemDTO<string>> DeleteUser(string? id);
    }
}
